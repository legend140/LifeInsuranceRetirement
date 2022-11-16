using LifeInsuranceRetirement.Core;
using LifeInsuranceRetirement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LifeInsuranceRetirement.Pages.Consumers
{
    public class EditModel : PageModel
    {
        private readonly IConsumerData consumerData;
        private readonly IBenefitData benefitData;

        [BindProperty]
        public Consumer? Consumer { get; set; }

        public IEnumerable<ConsumerLogs> ConsumerHistory { get; set; }

        [TempData]
        public string Message { get; set; }

        public string submitBtn {
            get
            {
                if (Consumer?.Id > 0)
                {
                    return "Update";
                } else
                {
                    return "Add";
                }
            }
        }

        public EditModel(IConsumerData consumerData, IBenefitData benefitData)
        {
            this.consumerData = consumerData;
            this.benefitData = benefitData;
        }
        public IActionResult OnGet(int? consumerId)
        {
            if (consumerId.HasValue)
            {
                Consumer = consumerData.GetById(consumerId.Value);
                ConsumerHistory = consumerData.GetLogs(consumerId.Value);
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
                    string user = "Ronito Asinjo Jr.";
                    LogType logType = LogType.Created;

                    if (Consumer.Id > 0)
                    {
                        Consumer.UpdatedBy = user;
                        Consumer.UpdatedDT = DateTime.Now;
                        logType = LogType.Updated;
                        Consumer = consumerData.Update(Consumer);
                        TempData["Message"] = "Update success!";
                    } else
                    {
                        Consumer.CreatedBy = user;
                        Consumer.CreatedDT = DateTime.Now;
                        Consumer = consumerData.Add(Consumer);
                        TempData["Message"] = "Add success!";
                    }

                    consumerData.Commit();

                    if (Consumer?.BenefitId > 0)
                    {
                        benefitData.UpdateUpdatedByAndUpdatedDT(new Benefit
                        {
                            Id = Consumer.BenefitId.Value,
                            UpdatedBy = user,
                            UpdatedDT = DateTime.Now
                        });
                    }

                    var benefit = benefitData.Add(new Benefit
                    {
                        ConsumerId = Consumer.Id,
                        CreatedBy = user,
                        CreatedDT = DateTime.Now,
                        UpdatedBy = user,
                        UpdatedDT = DateTime.Now
                    });
                    benefitData.Commit();

                    if (benefit != null)
                    {
                        Consumer.BenefitId = benefit.Id;
                        consumerData.Update(Consumer);
                    }

                    var addConsumerLogs = new ConsumerLogs
                    {
                        ConsumerId = Consumer.Id,
                        LogType = logType,
                        Name = Consumer.Name,
                        BasicSalary = Consumer.BasicSalary,
                        BirthDate = Consumer.BirthDate,
                        BenefitId = benefit?.Id ?? 0,
                        LoggedBy = user,
                        LoggedDT = DateTime.Now,
                    };

                    consumerData.AddLogs(addConsumerLogs);
                    consumerData.Commit();
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
