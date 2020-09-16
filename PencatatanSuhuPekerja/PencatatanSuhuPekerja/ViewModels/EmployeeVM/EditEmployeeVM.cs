using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.ViewModels.EmployeeVM
{
    public class EditEmployeeVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public string DivisionId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
