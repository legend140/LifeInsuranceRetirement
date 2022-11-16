using LifeInsuranceRetirement.Api.Controllers;
using LifeInsuranceRetirement.Data;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LifeInsuranceRetirement.Api.Tests
{
    [TestClass]
    public class ConsumerEndPointsTest
    {
        const string url = "https://localhost:7070/api/Consumer";

        public ConsumerEndPointsTest()
        {
        }

        [TestMethod]
        public async Task GetConsumersEndPointTest()
        {
        }
    }
}