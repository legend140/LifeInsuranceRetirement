using System.ComponentModel.DataAnnotations;

namespace LifeInsuranceRetirement.Core
{
    public class Benefits
    {
        public int ConfigurationId { get; set; }
        public int ConsumerId { get; set; }
        public Configuration? Configuration { get; set; }
        public Consumer? Consumer { get; set; }
        public int Multiple { get; set; }
        public int BenefitsAmountQuotation
        {
            get
            {
                return (Consumer?.BasicSalary ?? 0) * Multiple;
            }
        }

        public int PendedAmount
        {
            get
            {
                return Math.Max(0, BenefitsAmountQuotation - (Configuration?.GuaranteedIssue ?? 0));
            }
        }

        public string Status
        {
            get
            {
                if (Consumer?.Age >= Configuration?.MinAgeLimit && Consumer.Age <= Configuration.MaxAgeLimit)
                {
                    if (PendedAmount > 0)
                    {
                        return "For Approval";
                    }
                }

                return BenefitsAmountQuotation.ToString("#,##0");
            }
        }
    }
}
