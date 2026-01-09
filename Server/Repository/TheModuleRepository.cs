using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace AcMeCompany.Module.TheModule.Repository
{
    public interface ITheModuleRepository
    {
        IEnumerable<Models.TheModule> GetTheModules(int ModuleId);
        Models.TheModule GetTheModule(int TheModuleId);
        Models.TheModule GetTheModule(int TheModuleId, bool tracking);
        Models.TheModule AddTheModule(Models.TheModule TheModule);
        Models.TheModule UpdateTheModule(Models.TheModule TheModule);
        void DeleteTheModule(int TheModuleId);
    }

    public class TheModuleRepository : ITheModuleRepository, ITransientService
    {
        private readonly IDbContextFactory<TheModuleContext> _factory;

        public TheModuleRepository(IDbContextFactory<TheModuleContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.TheModule> GetTheModules(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.TheModule.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.TheModule GetTheModule(int TheModuleId)
        {
            return GetTheModule(TheModuleId, true);
        }

        public Models.TheModule GetTheModule(int TheModuleId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.TheModule.Find(TheModuleId);
            }
            else
            {
                return db.TheModule.AsNoTracking().FirstOrDefault(item => item.TheModuleId == TheModuleId);
            }
        }

        public Models.TheModule AddTheModule(Models.TheModule TheModule)
        {
            using var db = _factory.CreateDbContext();
            db.TheModule.Add(TheModule);
            db.SaveChanges();
            return TheModule;
        }

        public Models.TheModule UpdateTheModule(Models.TheModule TheModule)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(TheModule).State = EntityState.Modified;
            db.SaveChanges();
            return TheModule;
        }

        public void DeleteTheModule(int TheModuleId)
        {
            using var db = _factory.CreateDbContext();
            Models.TheModule TheModule = db.TheModule.Find(TheModuleId);
            db.TheModule.Remove(TheModule);
            db.SaveChanges();
        }
    }
}
