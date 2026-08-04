using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class NoticeMaster_NM
    {
        public long NM_Id { get; set; }

        public string NM_Code { get; set; }

        public long NM_NT_Id { get; set; }

        public string NT_Name { get; set; }

        public string NM_Title { get; set; }

        public string NM_Notice { get; set; }

        public DateTime? NM_FromDate { get; set; }

        public DateTime? NM_ToDate { get; set; }

        public string NM_PartyCode { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }

        public string PartyName { get; set; }

        public long? NM_AreaId { get; set; }

        public string AreaName { get; set; }

        public long? NM_ParaId { get; set; }

        public string ParaName { get; set; }

        public string NM_UploadFile { get; set; }

        public bool NM_IsPublish { get; set; }

        public DateTime NM_CreatedDate { get; set; }

        public int NM_CreatedBy { get; set; }

        public DateTime? NM_EditedDate { get; set; }

        public int? NM_EditedBy { get; set; }

        // Display Field
        public string NoticeFor { get; set; }
    }
}
