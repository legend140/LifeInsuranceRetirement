using LifeInsuranceRetirement.Core;
using LifeInsuranceRetirement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace LifeInsuranceRetirement.Pages.Consumers
{
    public class DeleteModel : PageModel
    {
        private readonly IConsumerData consumerData;
        public Consumer Consumer;

        public DeleteModel(IConsumerData consumerData)
        {
            this.consumerData = consumerData;
        }
        public IActionResult OnGet(int consumerId)
        {
            Consumer = consumerData.GetById(consumerId);
            if (Consumer == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
        public IActionResult OnPost(int consumerId)
        {
            var consumer = consumerData.Delete(consumerId);
            if (consumer == null)
            {
                return RedirectToPage("./NotFound");
            }

            TempData["Message"] = $"{consumer.Name} deleted";
            return RedirectToPage("./List");
        }
    }
}
