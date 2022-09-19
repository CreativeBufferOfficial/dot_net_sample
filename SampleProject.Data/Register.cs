using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace SampleProject.Data
{
    /// <summary>
    /// to resolve Data layer dependencies
    /// </summary>
    public static class Register
    {
        public static void ConfigureDataLayer(IServiceCollection services)
        {
            //auto register data for matching names 
            services.RegisterAssemblyPublicNonGenericClasses()
                    .Where(c => c.Name.EndsWith("Repository"))
                    .AsPublicImplementedInterfaces();
        }
    }
}