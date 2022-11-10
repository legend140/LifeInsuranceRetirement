using LifeInsuranceRetirement.Core;
using LifeInsuranceRetirement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LifeInsuranceRetirement.Pages
{
    public class SetupModel : PageModel
    {
        private readonly IConfigurationData configurationData;
        [BindProperty]
        public Configuration Configuration { get; set; }
        [TempData]
        public string Message { get; set; }

        public SetupModel(IConfigurationData configurationData)
        {
            this.configurationData = configurationData;
        }
        public void OnGet()
        {
            Configuration = configurationData.Get();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Configuration = configurationData.Save(Configuration);
                TempData["Message"] = "Save success!";
                return RedirectToPage("./Setup");
            }
            return Page();
        }
    }
}
