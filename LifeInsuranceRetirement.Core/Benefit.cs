using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LifeInsuranceRetirement.Core
{
    public class Benefit
    {
        [Key]
        public int Id { get; set; }
        public int ConfigurationId { get; set; }
        public int ConsumerId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Configuration? Configuration { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Consumer? Consumer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BenefitDetail>? BenefitDetails { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDT { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDT { get; set; }
        public Boolean IsDeleted { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ConsumerLogs>? ConsumerLogs { get; set; }
    }
}
