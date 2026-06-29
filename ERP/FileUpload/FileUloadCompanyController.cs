using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ERP.FileUpload
{
    public class FileUloadCompanyController : ApiController
    {
        [Route("api/FileUloadCompany/UploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Document/Img/Company/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            //Fetch the File Name.
            string fileName = HttpContext.Current.Request.Form["fileName"];
            //Save the File.
            postedFile.SaveAs(path + fileName);
            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
    }
}
