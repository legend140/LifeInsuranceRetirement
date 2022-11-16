using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static LifeInsuranceRetirement.Core.BenefitDetailProp;

namespace LifeInsuranceRetirement.Core
{
    public class BenefitDetailProp
    {
        public int BenefitId { get; set; }
        public int Multiple { get; set; }
        public int BenefitsAmountQuotation
        {
            get
            {
                return (Benefit?.Consumer?.BasicSalary ?? 0) * Multiple;
            }
        }
        public int PendedAmount
        {
            get
            {
                return Math.Max(0, BenefitsAmountQuotation - (Benefit?.Configuration?.GuaranteedIssue ?? 0));
            }
        }
        public BenefitStatus Status
        {
            get
            {
                if (Benefit?.Consumer?.Age >= Benefit?.Configuration?.MinAgeLimit && Benefit?.Consumer.Age <= Benefit?.Configuration.MaxAgeLimit)
                {
                    if (BenefitsAmountQuotation > (Benefit?.Configuration?.GuaranteedIssue ?? 0))
                    {
                        return BenefitStatus.ForApproval;
                    }
                }

                return BenefitStatus.Approved;
            }
        }

        public Benefit? Benefit { get; set; }
    }
}
