using LifeInsuranceRetirement.Core;
using LifeInsuranceRetirement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace LifeInsuranceRetirement.Pages.Consumers
{
    public class EditModel : PageModel
    {
        private readonly IConsumerData consumerData;
        [BindProperty]
        public Consumer? Consumer { get; set; }
        [TempData]
        public string Message { get; set; }

        public EditModel(IConsumerData consumerData)
        {
            this.consumerData = consumerData;
        }
        public IActionResult OnGet(int? consumerId)
        {
            if (consumerId.HasValue)
            {
                Consumer = consumerData.GetById(consumerId.Value);
            } else
            {
                Consumer = new Consumer();
            }
            if (Consumer == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Consumer != null)
                {
                    if (Consumer.Id > 0)
                    {
                        Consumer = consumerData.Update(Consumer);
                        TempData["Message"] = "Update success!";
                    } else
                    {
                        Consumer = consumerData.Add(Consumer);
                        TempData["Message"] = "Add success!";
                    }
                }

                if (Consumer == null)
                {
                    return RedirectToPage("./NotFound");
                }
                
                return RedirectToPage("./Edit", new { consumerId = Consumer.Id });
            }
            return Page();
        }
    }
}
