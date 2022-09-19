using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SampleProject.API.Infrastructure.Config;
using SampleProject.API.Infrastructure.Middlewares;
using SampleProject.Common.Infrastructure;
using SampleProject.Common.Infrastructure.Helper;
using SampleProject.Common.Infrastructure.Models.RequestModels.User;
using SampleProject.Service.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Configuration
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true, reloadOnChange: true);

    builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            // Adds a custom error response factory when ModelState is invalid
            options.InvalidModelStateResponseFactory = InvalidModelStateResponse.ProduceErrorResponse;
        })
        .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

    // adding Cross-Origin Resource Sharing (CORS) policy
    builder.Services.AddCors(x =>
        {
            var origins = builder.Configuration
                            .GetSection("AllowedHosts")
                            .Get<string[]>();

            x.AddPolicy("CorsPolicy",
                builder => builder
                            .AllowAnyMethod()
                            .WithOrigins(origins)
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .Build());
        });

    // adding JSON Web Token (JWT) authentication
    builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,

                ValidAudience = builder.Configuration.GetValue<string>("AppSettings:Audience"),
                ValidIssuer = builder.Configuration.GetValue<string>("AppSettings:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:JwtSymmatricKey").Value)),
            };
            x.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");

                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    }
                    return Task.CompletedTask;
                }
            };
        });

    builder.Services.AddMvc(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        })
        .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    // adding the DB connection
    builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(builder.Configuration.GetConnectionString("SampleProjectDbConnection")));

    // adding automapper​
    builder.Services.AddAutoMapper(SampleProject.Service.Register.GetAutoMapperProfiles());


    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // register DI in upper layers
    SampleProject.Service.Register.ConfigureServices(builder.Services);
}

var app = builder.Build();


// configure HTTP request pipeline
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // adding middlewares
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    app.UseCors("CorsPolicy");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}


// create hardcoded test users and roles in db on startup
{
    using var scope = app.Services.CreateScope();
    var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();
    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

    foreach (var item in Enum.GetValues(typeof(GlobalConstants.RolesEnum)))
    {
        await roleService.CreateRoleIfNotExists(Convert.ToInt32(item), item.ToString());
    }

    var usersToBeAdded = new List<CreateUserRequestModel>()
    {
        new CreateUserRequestModel { FirstName = "Admin", LastName = "Admin", Email = "admin@gmail.com",
                                        Password = "Admin@123", RoleId = (int)GlobalConstants.RolesEnum.Admin, IsActive = true },
        new CreateUserRequestModel { FirstName = "Employee", LastName = "Employee", Email = "employee@gmail.com",
                                        Password = "Employee@123", RoleId = (int)GlobalConstants.RolesEnum.Employee, IsActive = true },
    };

    foreach (var item in usersToBeAdded)
    {
        await userService.CreateUser(item, 0);
    }
}


app.Run();
