using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace AcMeCompany.Module.TheModule.Migrations.EntityBuilders
{
    public class TheModuleEntityBuilder : AuditableBaseEntityBuilder<TheModuleEntityBuilder>
    {
        private const string _entityTableName = "AcMeCompanyTheModule";
        private readonly PrimaryKey<TheModuleEntityBuilder> _primaryKey = new("PK_AcMeCompanyTheModule", x => x.TheModuleId);
        private readonly ForeignKey<TheModuleEntityBuilder> _moduleForeignKey = new("FK_AcMeCompanyTheModule_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public TheModuleEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override TheModuleEntityBuilder BuildTable(ColumnsBuilder table)
        {
            TheModuleId = AddAutoIncrementColumn(table,"TheModuleId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> TheModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
