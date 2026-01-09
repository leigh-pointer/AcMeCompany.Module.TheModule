using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace AcMeCompany.Module.TheModule.Services
{
    public interface ITheModuleService 
    {
        Task<List<Models.TheModule>> GetTheModulesAsync(int ModuleId);

        Task<Models.TheModule> GetTheModuleAsync(int TheModuleId, int ModuleId);

        Task<Models.TheModule> AddTheModuleAsync(Models.TheModule TheModule);

        Task<Models.TheModule> UpdateTheModuleAsync(Models.TheModule TheModule);

        Task DeleteTheModuleAsync(int TheModuleId, int ModuleId);
    }

    public class TheModuleService : ServiceBase, ITheModuleService
    {
        public TheModuleService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("TheModule");

        public async Task<List<Models.TheModule>> GetTheModulesAsync(int ModuleId)
        {
            List<Models.TheModule> TheModules = await GetJsonAsync<List<Models.TheModule>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.TheModule>().ToList());
            return TheModules.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.TheModule> GetTheModuleAsync(int TheModuleId, int ModuleId)
        {
            return await GetJsonAsync<Models.TheModule>(CreateAuthorizationPolicyUrl($"{Apiurl}/{TheModuleId}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.TheModule> AddTheModuleAsync(Models.TheModule TheModule)
        {
            return await PostJsonAsync<Models.TheModule>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, TheModule.ModuleId), TheModule);
        }

        public async Task<Models.TheModule> UpdateTheModuleAsync(Models.TheModule TheModule)
        {
            return await PutJsonAsync<Models.TheModule>(CreateAuthorizationPolicyUrl($"{Apiurl}/{TheModule.TheModuleId}", EntityNames.Module, TheModule.ModuleId), TheModule);
        }

        public async Task DeleteTheModuleAsync(int TheModuleId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{TheModuleId}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
