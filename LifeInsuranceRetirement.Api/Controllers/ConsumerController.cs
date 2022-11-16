using LifeInsuranceRetirement.Core;
using LifeInsuranceRetirement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LifeInsuranceRetirement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly ILogger<ConsumerController> _logger;
        private readonly IBenefitData _benefitData;
        private readonly IConsumerData _consumerData;

        public ConsumerController(ILogger<ConsumerController> logger, IBenefitData benefitData, IConsumerData consumerData)
        {
            _logger = logger;
            _benefitData = benefitData;
            _consumerData = consumerData;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consumer>>> Get(string? searchTerm)
        {
            var consumers = await _consumerData.GetConsumersByNameAsync(searchTerm ?? String.Empty);

            if (consumers == null)
            {
                return NotFound();
            }

            return Ok(consumers);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Consumer>> GetById(int Id)
        {
            var consumer = await _consumerData.GetByIdAsync(Id);

            if (consumer == null)
            {
                return NotFound();
            }

            return Ok(consumer);
        }

        [HttpGet("History/{consumerId}")]
        public async Task<ActionResult<IEnumerable<ConsumerLogs>>> GetHistory(int consumerId)
        {
            var history = await _consumerData.GetLogsAsync(consumerId);

            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConsumerDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string user = "Ronito Asinjo Jr."; // Hard Coded for now since didn't implement user login

            var consumer = await _consumerData.AddAsync(new Consumer
            {
                Name = value.Name,
                BasicSalary = value.BasicSalary,
                BirthDate = value.BirthDate,
                BenefitId = value.BenefitId,
                CreatedBy = user,
                CreatedDT = DateTime.Now
            });
            await _consumerData.CommitAsync();

            if (consumer.Id == 0)
            {
                return NotFound();
            }

            var benefit = await _benefitData.AddAsync(new Benefit
            {
                ConsumerId = consumer.Id,
                CreatedBy = user,
                CreatedDT = DateTime.Now
            });

            await _benefitData.CommitAsync();

            if (benefit != null)
            {
                consumer.BenefitId = benefit.Id;
                await _consumerData.UpdateAsync(consumer);
            }

            var addConsumerLogs = new ConsumerLogs
            {
                ConsumerId = consumer.Id,
                LogType = LogType.Created,
                Name = value.Name,
                BasicSalary = value.BasicSalary,
                BirthDate = value.BirthDate,
                BenefitId = benefit?.Id ?? 0,
                LoggedBy = user,
                LoggedDT = DateTime.Now,
            };

            await _consumerData.AddLogsAsync(addConsumerLogs);
            await _consumerData.CommitAsync();

            return Ok(consumer);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] ConsumerDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string user = "Ronito Asinjo Jr."; // Hard Coded for now since didn't implement user login

            var consumer = await _consumerData.UpdateAsync(new Consumer
            {
                Id = Id,
                Name = value.Name,
                BasicSalary = value.BasicSalary,
                BirthDate = value.BirthDate,
                BenefitId = value.BenefitId,
                UpdatedBy = user,
                UpdatedDT = DateTime.Now
            });
            await _consumerData.CommitAsync();
            if (consumer == null)
            {
                return NotFound();
            }

            if (consumer.BenefitId > 0)
            {
                await _benefitData.UpdateUpdatedByAndUpdatedDTAsync(new Benefit
                {
                    Id = consumer.BenefitId.Value,
                    UpdatedBy = user,
                    UpdatedDT = DateTime.Now
                });
            }

            var benefit = await _benefitData.AddAsync(new Benefit
            {
                ConsumerId = consumer.Id,
                CreatedBy = user,
                CreatedDT = DateTime.Now,
                UpdatedBy = user,
                UpdatedDT = DateTime.Now
            });
            await _benefitData.CommitAsync();

            if (benefit != null)
            {
                consumer.BenefitId = benefit.Id;
                await _consumerData.UpdateAsync(consumer);
            }

            var addConsumerLogs = new ConsumerLogs
            {
                ConsumerId = consumer.Id,
                LogType = LogType.Updated,
                Name = value.Name,
                BasicSalary = value.BasicSalary,
                BirthDate = value.BirthDate,
                BenefitId = benefit?.Id ?? 0,
                LoggedBy = user,
                LoggedDT = DateTime.Now,
            };

            await _consumerData.AddLogsAsync(addConsumerLogs);
            await _consumerData.CommitAsync();

            return Ok(consumer);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string user = "Ronito Asinjo Jr."; // Hard Coded for now since didn't implement user login

            var consumer = await _consumerData.DeleteAsync(Id);
            if (consumer == null)
            {
                return NotFound();
            }
            await _consumerData.CommitAsync();

            if (consumer.BenefitId > 0)
            {
                await _benefitData.DeleteAsync(consumer.BenefitId.Value);
                await _benefitData.CommitAsync();
            }

            var addConsumerLogs = new ConsumerLogs
            {
                ConsumerId = consumer.Id,
                LogType = LogType.Deleted,
                BasicSalary = consumer.BasicSalary,
                BirthDate = consumer.BirthDate,
                BenefitId = consumer?.BenefitId??0,
                LoggedBy = user,
                LoggedDT = DateTime.Now,
            };

            await _consumerData.AddLogsAsync(addConsumerLogs);
            await _consumerData.CommitAsync();

            return Ok(consumer);
        }
    }
}