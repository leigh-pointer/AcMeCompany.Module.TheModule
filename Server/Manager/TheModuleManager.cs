using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using AcMeCompany.Module.TheModule.Repository;
using System.Threading.Tasks;

namespace AcMeCompany.Module.TheModule.Manager
{
    public class TheModuleManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly ITheModuleRepository _TheModuleRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public TheModuleManager(ITheModuleRepository TheModuleRepository, IDBContextDependencies DBContextDependencies)
        {
            _TheModuleRepository = TheModuleRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new TheModuleContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new TheModuleContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.TheModule> TheModules = _TheModuleRepository.GetTheModules(module.ModuleId).ToList();
            if (TheModules != null)
            {
                content = JsonSerializer.Serialize(TheModules);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.TheModule> TheModules = null;
            if (!string.IsNullOrEmpty(content))
            {
                TheModules = JsonSerializer.Deserialize<List<Models.TheModule>>(content);
            }
            if (TheModules != null)
            {
                foreach(var TheModule in TheModules)
                {
                    _TheModuleRepository.AddTheModule(new Models.TheModule { ModuleId = module.ModuleId, Name = TheModule.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var TheModule in _TheModuleRepository.GetTheModules(pageModule.ModuleId))
           {
               if (TheModule.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "AcMeCompanyTheModule",
                       EntityId = TheModule.TheModuleId.ToString(),
                       Title = TheModule.Name,
                       Body = TheModule.Name,
                       ContentModifiedBy = TheModule.ModifiedBy,
                       ContentModifiedOn = TheModule.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
