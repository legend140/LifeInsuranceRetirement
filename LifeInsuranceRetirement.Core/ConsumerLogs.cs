using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LifeInsuranceRetirement.Core
{
    public class ConsumerLogs
    {
        [Key]
        public int Id { get; set; }
        public LogType LogType { get; set; }
        public int ConsumerId { get; set; }
        public string? Name { get; set; }
        public int BasicSalary { get; set; }
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public int BenefitId { get; set; }
        public string? LoggedBy { get; set; }
        public DateTime LoggedDT { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Benefit? Benefit { get; set; }
    }
}
