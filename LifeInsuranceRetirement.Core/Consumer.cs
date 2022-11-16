using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LifeInsuranceRetirement.Core
{
    public class Consumer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public int BasicSalary { get; set; }
        [Required, Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Benefit>? Benefits { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Benefit? Benefit { get; set; }
        [ForeignKey("Benefit")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? BenefitId { get; set; }
        public int Age
        {
            get
            {
                DateTime now = DateTime.Now;
                int age = now.Year - BirthDate.Year;
                if (now.Month < BirthDate.Month || (now.Month == BirthDate.Month && now.Day < BirthDate.Day))
                {
                    age--;
                }
                return age;
            }
        }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDT { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDT { get; set; }
        public Boolean IsDeleted { get; set; }

    }
}
