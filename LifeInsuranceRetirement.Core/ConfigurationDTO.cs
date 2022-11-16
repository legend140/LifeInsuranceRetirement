using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LifeInsuranceRetirement.Core
{
    public class ConfigurationDTO
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Guaranteed Issue should be greater than or equal to 1 and less than or equal to 1000000.")]
        public int GuaranteedIssue { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Max Age Limit should be greater than or equal to 0 and less than or equal to 100.")]
        public int MaxAgeLimit { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Min Age Limit should be greater than or equal to 0 and less than or equal to 100.")]
        public int MinAgeLimit { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Min Range should be greater than or equal to 1 and less than or equal to 100.")]
        public int MinRange { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Max Range should be greater than or equal to 1 and less than or equal to 100.")]
        public int MaxRange { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Increments should be greater than or equal to 1 and less than or equal to 100.")]
        public int Increments { get; set; }
    }
}