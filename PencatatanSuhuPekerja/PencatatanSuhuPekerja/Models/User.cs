using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PencatatanSuhuPekerjaAPI.Models
{
    [Table("tb_m_user")]
    public class User : IdentityUser
    {
        public Employee Employee { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
