using LifeInsuranceRetirement.Core;
using LifeInsuranceRetirement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LifeInsuranceRetirement.Pages
{
    public class SetupModel : PageModel
    {
        private readonly IConfigurationData configurationData;
        [BindProperty]
        public Configuration? Configuration { get; set; }
        [TempData]
        public string Message { get; set; }

        public SetupModel(IConfigurationData configurationData)
        {
            this.configurationData = configurationData;
        }
        public void OnGet()
        {
            Configuration = configurationData.Get();
            if (Configuration == null)
            {
                Configuration = new Configuration();
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string user = "Ronito Asinjo Jr.";
            LogType logType = LogType.Created;

            if (Configuration?.Id > 0)
            {
                Configuration.UpdatedBy = user;
                Configuration.UpdatedDT = DateTime.Now;
                logType = LogType.Updated;
                Configuration = configurationData.Update(Configuration);
            } 
            else
            {
                Configuration.CreatedBy = user;
                Configuration.CreatedDT = DateTime.Now;
                Configuration = configurationData.Add(Configuration);
            }

            var addConfigLogs = new ConfigurationLogs
            {
                ConfigurationId = Configuration.Id,
                LogType = logType,
                GuaranteedIssue = Configuration.GuaranteedIssue,
                MaxAgeLimit = Configuration.MaxAgeLimit,
                MinAgeLimit = Configuration.MinAgeLimit,
                MinRange = Configuration.MinRange,
                MaxRange = Configuration.MaxRange,
                Increments = Configuration.Increments,
                LoggedBy = user,
                LoggedDT = DateTime.Now,
            };

            configurationData.AddLogs(addConfigLogs);

            configurationData.Commit();
            TempData["Message"] = "Save success!";
            return RedirectToPage("./Setup");
        }
    }
}
