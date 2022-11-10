using Newtonsoft.Json;
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
        public ICollection<Benefits>? Benefits { get; set; }
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
    }
}
