using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Models
{
    [Table("temperatures")]
    public class Temperature
    {
        [Key]
        public string TemperatureId { get; set; }
        public DateTime Date { get; set; }
    }
}
