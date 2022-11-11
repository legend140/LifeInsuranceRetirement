using LifeInsuranceRetirement.Core;

namespace LifeInsuranceRetirment.Core.Tests
{
    [TestClass]
    public class ConsumerTest
    {
        [TestMethod]
        [Description("Check if age is computed correctly based on Birth Date")]
        public void CheckIfAgeIsCorrectBasedOnBirthday()
        {
            var consumer = new Consumer();

            const int age = 13;

            consumer.BirthDate = new DateTime(DateTime.Now.Year - 13, 1, 1);

            Assert.AreEqual(age, consumer.Age);
        }
    }
}