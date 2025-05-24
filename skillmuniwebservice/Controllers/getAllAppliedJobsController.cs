using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m2ostnextservice.Models;
using Newtonsoft.Json;

namespace m2ostnextservice.Controllers
{
    public class getAllAppliedJobsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult AppliedJob(int UID, int OID)
        {
            List<tbl_job_port> tblJobPortList = new List<tbl_job_port>();
            List<tbl_job_user_log> tblJobUserLogList = new List<tbl_job_user_log>();

            try
            {
                
                using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                {
                    // Get list of jobs
                    tblJobPortList = db.Database.SqlQuery<tbl_job_port>(
                        "SELECT * FROM tbl_job WHERE save_type={0} ORDER BY id_job DESC", "P"
                    ).ToList();

                    // Get list of applied jobs
                    tblJobUserLogList = db.Database.SqlQuery<tbl_job_user_log>(
                        "SELECT * FROM tbl_job_user_log INNER JOIN tbl_job ON tbl_job_user_log.id_job = tbl_job.id_job WHERE tbl_job_user_log.id_user={0}", UID
                    ).ToList();

                    // Get applied count for each job
                    foreach (tbl_job_port tblJobPort in tblJobPortList)
                    {
                        tblJobPort.AppliedCount = db.Database.SqlQuery<int>(
                            "SELECT COUNT(*) FROM tbl_job_user_log WHERE id_job = {0}", tblJobPort.id_job
                        ).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            // Create response object
            var result = new
            {
                AppliedJobs = tblJobUserLogList
            };

            return Ok(result);
        }

    }
}
