using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using AcMeCompany.Module.TheModule.Repository;

namespace AcMeCompany.Module.TheModule.Services
{
    public class ServerTheModuleService : ITheModuleService
    {
        private readonly ITheModuleRepository _TheModuleRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerTheModuleService(ITheModuleRepository TheModuleRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _TheModuleRepository = TheModuleRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.TheModule>> GetTheModulesAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_TheModuleRepository.GetTheModules(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.TheModule> GetTheModuleAsync(int TheModuleId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_TheModuleRepository.GetTheModule(TheModuleId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Get Attempt {TheModuleId} {ModuleId}", TheModuleId, ModuleId);
                return null;
            }
        }

        public Task<Models.TheModule> AddTheModuleAsync(Models.TheModule TheModule)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, TheModule.ModuleId, PermissionNames.Edit))
            {
                TheModule = _TheModuleRepository.AddTheModule(TheModule);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "TheModule Added {TheModule}", TheModule);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Add Attempt {TheModule}", TheModule);
                TheModule = null;
            }
            return Task.FromResult(TheModule);
        }

        public Task<Models.TheModule> UpdateTheModuleAsync(Models.TheModule TheModule)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, TheModule.ModuleId, PermissionNames.Edit))
            {
                TheModule = _TheModuleRepository.UpdateTheModule(TheModule);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "TheModule Updated {TheModule}", TheModule);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Update Attempt {TheModule}", TheModule);
                TheModule = null;
            }
            return Task.FromResult(TheModule);
        }

        public Task DeleteTheModuleAsync(int TheModuleId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _TheModuleRepository.DeleteTheModule(TheModuleId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "TheModule Deleted {TheModuleId}", TheModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Delete Attempt {TheModuleId} {ModuleId}", TheModuleId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
