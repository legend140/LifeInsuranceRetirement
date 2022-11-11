using LifeInsuranceRetirement.Core;
using LifeInsuranceRetirement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LifeInsuranceRetirement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly ILogger<ConsumerController> _logger;
        private readonly IConsumerData _consumerData;

        public ConsumerController(ILogger<ConsumerController> logger, IConsumerData consumerData)
        {
            _logger = logger;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConsumerDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consumer = await _consumerData.AddAsync(new Consumer
            {
                Name = value.Name,
                BasicSalary = value.BasicSalary,
                BirthDate = value.BirthDate
            });

            return Ok(consumer);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] ConsumerDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consumer = await _consumerData.UpdateAsync(new Consumer
            {
                Id = Id,
                Name = value.Name,
                BasicSalary = value.BasicSalary,
                BirthDate = value.BirthDate
            });

            if (consumer == null)
            {
                return NotFound();
            }

            return Ok(consumer);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consumer = await _consumerData.DeleteAsync(Id);

            if (consumer == null)
            {
                return NotFound();
            }

            return Ok(consumer);
        }
    }
}