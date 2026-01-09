using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Oqtane.Services;
using AcMeCompany.Module.TheModule.Services;

namespace AcMeCompany.Module.TheModule.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            if (!services.Any(s => s.ServiceType == typeof(ITheModuleService)))
            {
                services.AddScoped<ITheModuleService, TheModuleService>();
            }
        }
    }
}
