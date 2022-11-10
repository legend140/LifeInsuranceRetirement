using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LifeInsuranceRetirement.Core
{
    public class Configuration
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "GuaranteedIssue should be greater than 0.")]
        public int? GuaranteedIssue { get; set; }
        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "MaxAgeLimit should be greater than or equal to 0.")]
        public int? MaxAgeLimit { get; set; }
        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "MinAgeLimit should be greater than or equal to 0.")]
        public int? MinAgeLimit { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "MinRange should be greater than or equal to 1.")]
        public int? MinRange { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "MaxRange should be greater than or equal to 1.")]
        public int? MaxRange { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Increments should be greater than or equal to 1.")]
        public int? Increments { get; set; }
    }
}