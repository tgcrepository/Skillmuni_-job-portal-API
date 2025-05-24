using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http;
using m2ostnextservice.Models;

namespace m2ostnextservice.Controllers
{
    public class VideoResumePostController : ApiController
    {


        //public HttpResponseMessage Get(int OID)
        public IHttpActionResult Post(int UID, int OID)
        {
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files["resumepath"];
                    string extension = Path.GetExtension(file.FileName);

                    if (file != null)
                    {
                        string resumeDirectory = HttpContext.Current.Server.MapPath("~/Content/VideoResume");
                        if (!Directory.Exists(resumeDirectory))
                        {
                            Directory.CreateDirectory(resumeDirectory);
                        }

                        string filename = Path.Combine(resumeDirectory, UID.ToString() + extension);
                        file.SaveAs(filename);

                        string resumeLocation = UID.ToString() + extension;
                        using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                        {

                            tbl_cv_master mas = db.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", UID, 1).FirstOrDefault();
                            if (mas == null)
                            {
                                int id_cv = db.Database.SqlQuery<int>(" insert into  tbl_cv_master (id_user,oid,created_date,modified_date,status,cv_type) values({0},{1},{2},{3},{4},{5});select max(id_cv) from tbl_cv_master", UID, OID, DateTime.Now, DateTime.Now, "A", 1).FirstOrDefault();
                                db.Database.ExecuteSqlCommand("insert into tbl_video_cv (id_cv,videoname,extn,status) values({0},{1},{2},{3})", id_cv, UID, extension, "P");


                            }
                            else
                            {

                                db.Database.ExecuteSqlCommand("update tbl_cv_master set modified_date={0} where id_cv={1}", DateTime.Now, mas.id_cv);
                                db.Database.ExecuteSqlCommand("update tbl_video_cv set extn={0} where id_cv={1}", extension, mas.id_cv);


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
