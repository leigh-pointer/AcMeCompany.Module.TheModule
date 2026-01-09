using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace AcMeCompany.Module.TheModule.Models
{
    [Table("AcMeCompanyTheModule")]
    public class TheModule : ModelBase
    {
        [Key]
        public int TheModuleId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
    }
}
