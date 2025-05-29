using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using m2ostnextservice.Models;

namespace m2ostnextservice.Controllers
{
    public class getAllJobsController : ApiController
    {
        //public HttpResponseMessage Get(int OID)
        public HttpResponseMessage Get(int UID, int OID)
        {
            List<JobsResponce> jobs = new List<JobsResponce>();
            List<tbl_job> tbl_Jobs = new List<tbl_job>();
            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                if (OID > 0)
                {
                    tbl_Jobs = db.Database.SqlQuery<tbl_job>("select * from tbl_job where status='A' AND tbl_organization_id='" + OID + "';").ToList();
                    foreach (var item in tbl_Jobs)
                    {
                        item.loc = db.Database.SqlQuery<string>("SELECT name FROM cities ct INNER JOIN tbl_job_location_mapping ctm ON ct.id= ctm.city where ctm.id_job = {0};", item.id_job).ToList();
                        if (item.applyflag == 0)
                        {
                            if (db.Database.SqlQuery<int>("select id_job_log from tbl_job_user_log where id_user={0} and id_job={1}", UID, item.id_job).FirstOrDefault<int>() > 0)
                                item.applyflag = 1;
                        }
                    }
                }
                else
                {
                    tbl_Jobs = db.Database.SqlQuery<tbl_job>("select * from tbl_job where status='A'").ToList();
                    foreach (var item in tbl_Jobs)
                    {
                        item.loc = db.Database.SqlQuery<string>("SELECT name FROM cities ct INNER JOIN tbl_job_location_mapping ctm ON ct.id= ctm.city where ctm.id_job = {0};", item.id_job).ToList();
                        if (item.applyflag == 0)
                        {
                            if (db.Database.SqlQuery<int>("select id_job_log from tbl_job_user_log where id_user={0} and id_job={1}", UID, item.id_job).FirstOrDefault<int>() > 0)
                                item.applyflag = 1;
                        }
                    }
                }


            }
            return Request.CreateResponse(HttpStatusCode.OK, tbl_Jobs);
        }

        [HttpGet]
        public IHttpActionResult JobPreferenceCount(int UID)
        {
            int PreferenceCount = 0;
            try
            {

                using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                {
                    PreferenceCount = db.Database.SqlQuery<int>("select COUNT(*) from  tbl_user_job_preferences where id_user={0} ", UID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            // Create response object
            var result = new
            {
                JobPreferenceCount = PreferenceCount
            };

            return Ok(result);
        }
    }
}
