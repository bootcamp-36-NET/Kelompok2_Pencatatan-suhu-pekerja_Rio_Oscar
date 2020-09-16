using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.ViewModels.AccountVM
{
    public class UserProfileVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
