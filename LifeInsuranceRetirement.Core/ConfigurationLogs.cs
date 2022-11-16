using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LifeInsuranceRetirement.Core
{
    public class ConfigurationLogs
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public LogType LogType { get; set; }
        public int ConfigurationId { get; set; }
        public int GuaranteedIssue { get; set; }
        public int MaxAgeLimit { get; set; }
        public int MinAgeLimit { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        public int Increments { get; set; }
        public string? LoggedBy { get; set; }
        public DateTime LoggedDT { get; set; }
    }
}