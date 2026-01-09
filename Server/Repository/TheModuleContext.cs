using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace AcMeCompany.Module.TheModule.Repository
{
    public class TheModuleContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.TheModule> TheModule { get; set; }

        public TheModuleContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.TheModule>().ToTable(ActiveDatabase.RewriteName("AcMeCompanyTheModule"));
        }
    }
}
