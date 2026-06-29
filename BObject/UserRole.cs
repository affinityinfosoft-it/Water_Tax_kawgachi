using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BObject
{
    public class UserRole
    {
        public Int32 UserId { get; set; }
        public Int32 RoleId { get; set; }
        public Int32 ProjectId { get; set; }
        public string OppType { get; set; }
    }
}