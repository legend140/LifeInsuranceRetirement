using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInsuranceRetirement.Core
{
    public class ConsumerDTO
    {
        public string Name { get; set; }
        public int BasicSalary { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
