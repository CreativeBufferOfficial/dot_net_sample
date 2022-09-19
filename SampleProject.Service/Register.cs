using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace SampleProject.Service
{
    /// <summary>
    /// to resolve Service layer dependencies
    /// </summary>
    public static class Register
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //auto register services for matching names 
            services.RegisterAssemblyPublicNonGenericClasses()
                    .Where(c => c.Name.EndsWith("Service"))
                    .AsPublicImplementedInterfaces();

            // register dependent data layer
            Data.Register.ConfigureDataLayer(services);
        }

        /// <summary>
        /// to implement auto mapper profiles
        /// </summary>
        /// <returns></returns>
        public static Type[] GetAutoMapperProfiles()
        {
            //auto add all Profiles implementing Profile
            var typelist = typeof(Register).Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Profile)))
                .ToList();

            //add any extra here
            return typelist.ToArray();
        }
    }
}
