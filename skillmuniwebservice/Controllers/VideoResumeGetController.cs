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
    public class VideoResumeGetController : ApiController
    {

        public IHttpActionResult Get(int UID, int OID)
        {
            try
            {
                int resumeFlag = 0;
                string resumeLocation = "";

                using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                {
                    tbl_cv_master mas = db.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", UID, 1).FirstOrDefault();
                    if (mas != null)
                    {
                        resumeFlag = 1;
                        string extn = db.Database.SqlQuery<string>("select extn from tbl_video_cv where id_cv={0}", mas.id_cv).FirstOrDefault();
                        resumeLocation =UID+extn;
                    }
                }
                string resumePath = ConfigurationManager.AppSettings["VidResumePath"] +"/" +resumeLocation;
                return Ok(new
                {
                    resumeflag = resumeFlag,
                    resumename = resumeLocation,
                    resumelink = resumePath
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

       
    }
}
