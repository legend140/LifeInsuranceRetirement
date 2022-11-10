using LifeInsuranceRetirement.Core;
using LifeInsuranceRetirement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LifeInsuranceRetirement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetupController : ControllerBase
    {
        private readonly ILogger<SetupController> _logger;
        private readonly IConfigurationData _configurationData;

        public SetupController(ILogger<SetupController> logger, IConfigurationData configurationData)
        {
            _logger = logger;
            _configurationData = configurationData;
        }

        [HttpGet]
        public async Task<ActionResult<Configuration>> Get()
        {
            var configuration = await _configurationData.GetAsync();

            if (configuration == null)
            {
                return NotFound();
            }

            return Ok(configuration);
        }

        [HttpPost]
        public async Task<ActionResult<Configuration>> Post([FromBody] Configuration value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configuration = await _configurationData.SaveAsync(value);
            return Ok(configuration);
        }

        [HttpPut]
        public async Task<ActionResult<Configuration>> Put([FromBody] Configuration value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configuration = await _configurationData.SaveAsync(value);
            return Ok(configuration);
        }
    }
}