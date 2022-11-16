using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LifeInsuranceRetirement.Core
{
    public partial class BenefitDetail
    {
        [JsonIgnore]
        public int BenefitId { get; set; }
        public int Multiple { get; set; }
        public int BenefitsAmountQuotation { get; set; }
        public int PendedAmount { get; set; }
        public BenefitStatus Status { get; set; }

        public Benefit? Benefit { get; set; }
    }
}
