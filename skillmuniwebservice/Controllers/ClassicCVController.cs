using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using m2ostnextservice.Models;
using Newtonsoft.Json;

namespace m2ostnextservice.Controllers
{
    public class ClassicCVController : ApiController
    {

        [HttpGet]
        [Route("api/getClassicResume")]
        public IHttpActionResult GetResumeDetails(int UID, int OID)
        {
            try
            {
                int resumeFlag = 0;
                string resumePath = null;


                using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                {
                   tbl_cv_master mas = db.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", UID, 2).FirstOrDefault();


                    if (mas.isUploaded != null)
                    {
                        resumeFlag = 1;
                        resumePath = ConfigurationManager.AppSettings["ClassicResume"] + "/" + UID + ".pdf";

                    }
                }
               
                return Ok(new
                {
                    resumeflag = resumeFlag,
                    resumelink = resumePath
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/uploadClassicResume")]
        public IHttpActionResult UploadResumeAction(int UID, int OID)
        {
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files["resumepath"];
                    string extension = Path.GetExtension(file.FileName);

                    if (file != null)
                    {
                        string resumeDirectory = HttpContext.Current.Server.MapPath("~/Content/ClassicResume");
                        if (!Directory.Exists(resumeDirectory))
                        {
                            Directory.CreateDirectory(resumeDirectory);
                        }

                        string filename = Path.Combine(resumeDirectory, UID.ToString() + extension);
                        file.SaveAs(filename);
                        string resumeLocation = UID.ToString() + extension;
                        using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                        {
                            tbl_cv_master mas = db.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", UID, 2).FirstOrDefault();


                            if (mas != null)
                            {
                                file.SaveAs(filename);
                                db.Database.ExecuteSqlCommand("update tbl_cv_master set isUploaded = 1 where id_cv={1}", DateTime.Now, mas.id_cv);

                            }
                        }
                        return Ok(new { message = "Resume uploaded successfully", resumeLocation });
                    }
                    else
                    {
                        return BadRequest("No file found in the request.");
                    }
                }
                else
                {
                    return BadRequest("No files received.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
