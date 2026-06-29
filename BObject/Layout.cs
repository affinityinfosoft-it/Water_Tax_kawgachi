using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class Layout :Common
    {
//LayoutID, LayoutName, Head, Body, HeadCSS, BodyJS, Footer, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, IsActive
        public Int64 LayoutID { get; set; }
        public string LayoutName { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public string HeadCSS { get; set; }
        public string BodyJS { get; set; }
        public string Footer { get; set; }
        public IEnumerable<Layout> LayoutList { get; set; }

    }
}
