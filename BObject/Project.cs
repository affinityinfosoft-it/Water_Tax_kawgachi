using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectShortName { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string PhNo { get; set; }
        public string TelPhNo { get; set; }
        public string WebSite { get; set; }
        public string Address { get; set; }
        public string Pin { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public string GST { get; set; }
        public string Img { get; set; }
    }
}
