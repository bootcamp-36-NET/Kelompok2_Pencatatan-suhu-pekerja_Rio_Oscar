using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Models
{
    [Table("tb_m_employee")]
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public bool IsActive { get; set; }


        public User User { get; set; }

        public string DivisionId { get; set; }
        [ForeignKey("DivisionId")]
        public Division Division { get; set; }
    }
}
