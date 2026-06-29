using BObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class UserRoleDetails
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int ProjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Int64 CM_ID { get; set; }
        public int FyId { get; set; }
        public List<UserWiseCompany> LstCompany { get; set; }
    }
}