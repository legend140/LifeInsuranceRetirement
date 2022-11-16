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
        public async Task<ActionResult<Configuration>> Post([FromBody] ConfigurationDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string user = "Ronito Asinjo Jr."; // Hard Coded for now since didn't implement user login

            var configuration = await _configurationData.AddAsync(new Configuration
            {
                GuaranteedIssue = value.GuaranteedIssue,
                MaxAgeLimit = value.MaxAgeLimit,
                MinAgeLimit = value.MinAgeLimit,
                MinRange = value.MinRange,
                MaxRange = value.MaxRange,
                Increments = value.Increments,
                CreatedBy = user,
                CreatedDT = DateTime.Now
            });
            await _configurationData.CommitAsync();

            var addConfigLogs = new ConfigurationLogs
            {
                ConfigurationId = configuration.Id,
                LogType = LogType.Created,
                GuaranteedIssue = value.GuaranteedIssue,
                MaxAgeLimit = value.MaxAgeLimit,
                MinAgeLimit = value.MinAgeLimit,
                MinRange = value.MinRange,
                MaxRange = value.MaxRange,
                Increments = value.Increments,
                LoggedBy = user,
                LoggedDT = DateTime.Now,
            };

            await _configurationData.AddLogsAsync(addConfigLogs);
            await _configurationData.CommitAsync();

            return Ok(configuration);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Configuration>> Put([FromBody] ConfigurationDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string user = "Ronito Asinjo Jr."; // Hard Coded for now since didn't implement user login

            var configuration = await _configurationData.UpdateAsync(new Configuration
            {
                Id = value.Id,
                GuaranteedIssue = value.GuaranteedIssue,
                MaxAgeLimit = value.MaxAgeLimit,
                MinAgeLimit = value.MinAgeLimit,
                MinRange = value.MinRange,
                MaxRange = value.MaxRange,
                Increments = value.Increments,
                UpdatedBy = user,
                UpdatedDT = DateTime.Now
            });
            if (configuration == null)
            {
                return NotFound();
            }
            await _configurationData.CommitAsync();

            var addConfigLogs = new ConfigurationLogs
            {
                ConfigurationId = configuration.Id,
                LogType = LogType.Updated,
                GuaranteedIssue = value.GuaranteedIssue,
                MaxAgeLimit = value.MaxAgeLimit,
                MinAgeLimit = value.MinAgeLimit,
                MinRange = value.MinRange,
                MaxRange = value.MaxRange,
                Increments = value.Increments,
                LoggedBy = user,
                LoggedDT = DateTime.Now,
            };

            await _configurationData.AddLogsAsync(addConfigLogs);
            await _configurationData.CommitAsync();

            return Ok(configuration);
        }
    }
}