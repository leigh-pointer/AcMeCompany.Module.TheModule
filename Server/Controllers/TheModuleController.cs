using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using AcMeCompany.Module.TheModule.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace AcMeCompany.Module.TheModule.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class TheModuleController : ModuleControllerBase
    {
        private readonly ITheModuleService _TheModuleService;

        public TheModuleController(ITheModuleService TheModuleService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _TheModuleService = TheModuleService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Models.TheModule>> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _TheModuleService.GetTheModulesAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.TheModule> Get(int id, int moduleid)
        {
            Models.TheModule TheModule = await _TheModuleService.GetTheModuleAsync(id, moduleid);
            if (TheModule != null && IsAuthorizedEntityId(EntityNames.Module, TheModule.ModuleId))
            {
                return TheModule;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Get Attempt {TheModuleId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.TheModule> Post([FromBody] Models.TheModule TheModule)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, TheModule.ModuleId))
            {
                TheModule = await _TheModuleService.AddTheModuleAsync(TheModule);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Post Attempt {TheModule}", TheModule);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                TheModule = null;
            }
            return TheModule;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.TheModule> Put(int id, [FromBody] Models.TheModule TheModule)
        {
            if (ModelState.IsValid && TheModule.TheModuleId == id && IsAuthorizedEntityId(EntityNames.Module, TheModule.ModuleId))
            {
                TheModule = await _TheModuleService.UpdateTheModuleAsync(TheModule);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Put Attempt {TheModule}", TheModule);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                TheModule = null;
            }
            return TheModule;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            Models.TheModule TheModule = await _TheModuleService.GetTheModuleAsync(id, moduleid);
            if (TheModule != null && IsAuthorizedEntityId(EntityNames.Module, TheModule.ModuleId))
            {
                await _TheModuleService.DeleteTheModuleAsync(id, TheModule.ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized TheModule Delete Attempt {TheModuleId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
