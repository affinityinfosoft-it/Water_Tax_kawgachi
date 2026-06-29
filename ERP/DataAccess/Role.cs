using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.DataAccess
{
    public class Role
    {
        public int RoleId { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        public Boolean RegNewUser { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}