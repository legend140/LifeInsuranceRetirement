using LifeInsuranceRetirement.Core;
using LifeInsuranceRetirement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeInsuranceRetirement.Pages.Consumers
{
    public class ListModel : PageModel
    {
        private readonly IConsumerData consumerData;
        public IEnumerable<Consumer> Consumers { get; set;  }

        public ListModel(IConsumerData consumerData)
        {
            this.consumerData = consumerData;
        }
        public void OnGet(string searchTerm)
        {
            Consumers = consumerData.GetConsumersByName(searchTerm);
        }
    }
}
