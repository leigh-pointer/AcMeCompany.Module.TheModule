using Oqtane.Models;
using Oqtane.Modules;

namespace AcMeCompany.Module.TheModule
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "TheModule",
            Description = "AcMeCompany TheModule",
            Version = "1.0.0",
            ServerManagerType = "AcMeCompany.Module.TheModule.Manager.TheModuleManager, AcMeCompany.Module.TheModule.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "AcMeCompany.Module.TheModule.Shared.Oqtane",
            PackageName = "AcMeCompany.Module.TheModule" 
        };
    }
}
