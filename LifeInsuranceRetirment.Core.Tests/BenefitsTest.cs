using LifeInsuranceRetirement.Core;

namespace LifeInsuranceRetirment.Core.Tests
{
    [TestClass]
    public class BenefitsTest
    {
        private Consumer consumer;
        private Configuration configuration;

        [TestInitialize]
        public void Initialize()
        {
            configuration = new Configuration
            {
                Id = 1,
                GuaranteedIssue = 50000,
                MaxAgeLimit = 55,
                MinAgeLimit = 25,
                MinRange = 1,
                MaxRange = 5,
                Increments = 1
            };

            consumer = new Consumer
            {
                Id = 1,
                Name = "Scott's Pizza",
                BasicSalary = 80000,
                BirthDate = new DateTime(1980, 06, 01, 0, 0, 0)
            };
        }

        [TestMethod]
        [Description("Check if Benefits Amount Quotation is correct")]
        public void CheckIfBenefitsAmountQuotationIsCorrect()
        {
            const int expectedBenefitsAmountQuotation = 160000;
            const int multiple = 2;
            var benefits = new List<Benefits>();

            benefits.Add(new Benefits
            {
                ConsumerId = consumer.Id,
                Consumer = consumer,
                ConfigurationId = configuration.Id,
                Configuration = configuration,
                Multiple = multiple
            });

            consumer.Benefits = benefits;

            Assert.AreEqual(consumer.Benefits?.FirstOrDefault()?.BenefitsAmountQuotation, expectedBenefitsAmountQuotation);
        }

        [TestMethod]
        [Description("Check if PendedAmount is correct")]
        public void CheckIfPendedAmountIsCorrect()
        {
            const int expectedPendedAmount = 30000;
            const int multiple = 1;

            var benefits = new List<Benefits>();

            benefits.Add(new Benefits
            {
                ConsumerId = consumer.Id,
                Consumer = consumer,
                ConfigurationId = configuration.Id,
                Configuration = configuration,
                Multiple = multiple
            });

            consumer.Benefits = benefits;

            Assert.AreEqual(consumer.Benefits?.FirstOrDefault()?.PendedAmount, expectedPendedAmount);
        }

        [TestMethod]
        [Description("Check if Benefits Status is For Approval")]
        public void CheckIfBenefitsStatusIsForApproval()
        {
            const string expectedBenefitsStatus = "For Approval";
            const int multiple = 1;

            var benefits = new List<Benefits>();

            benefits.Add(new Benefits
            {
                ConsumerId = consumer.Id,
                Consumer = consumer,
                ConfigurationId = configuration.Id,
                Configuration = configuration,
                Multiple = multiple
            });

            consumer.Benefits = benefits;

            Assert.AreEqual(consumer.Benefits?.FirstOrDefault()?.Status, expectedBenefitsStatus);
        }

        [TestMethod]
        [Description("Check if Benefits Status is Approved and should return the Amount since there is nothing to Pend.")]
        public void CheckIfBenefitsStatusIsApproved()
        {
            const string expectedBenefitsStatus = "5,000";
            const int multiple = 1;

            configuration.GuaranteedIssue = 15000;
            consumer.BasicSalary = 5000;

            var benefits = new List<Benefits>();

            benefits.Add(new Benefits
            {
                ConsumerId = consumer.Id,
                Consumer = consumer,
                ConfigurationId = configuration.Id,
                Configuration = configuration,
                Multiple = multiple
            });

            consumer.Benefits = benefits;

            Assert.AreEqual(consumer.Benefits?.FirstOrDefault()?.Status, expectedBenefitsStatus);
        }

        [TestMethod]
        [Description("Check if Age is not within range, benefits will automatically approve and nothing will Pend.")]
        public void CheckIfAgeNotWithinRangeBenefitsAutomaticallyApproved()
        {
            const string expectedBenefitsStatus = "25,000";
            const int multiple = 5;

            configuration.GuaranteedIssue = 15000;
            consumer.BasicSalary = 5000;
            consumer.BirthDate = DateTime.Now;

            var benefits = new List<Benefits>();

            benefits.Add(new Benefits
            {
                ConsumerId = consumer.Id,
                Consumer = consumer,
                ConfigurationId = configuration.Id,
                Configuration = configuration,
                Multiple = multiple
            });

            consumer.Benefits = benefits;

            Assert.AreEqual(consumer.Benefits?.FirstOrDefault()?.Status, expectedBenefitsStatus);
        }
    }
}
