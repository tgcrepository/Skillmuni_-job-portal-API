using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m2ostnextservice.Models;

namespace m2ostnextservice.Controllers
{
    public class getJobCatListController : ApiController
    {
        public HttpResponseMessage Get([FromUri] int[] IndustryID)
        {
            List<tbl_job_category> cat = new List<tbl_job_category>();

            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                if (IndustryID == null || IndustryID.Length == 0)
                {
                    // Return all job categories if IndustryID is null or empty
                    cat = db.Database.SqlQuery<tbl_job_category>("SELECT * FROM tbl_job_category WHERE status='A'").ToList();
                }
                else
                {
                    // Filter job categories based on IndustryID array
                    string idList = string.Join(",", IndustryID);
                    cat = db.Database.SqlQuery<tbl_job_category>(
                        $"SELECT * FROM tbl_job_category WHERE status='A' AND id_industry IN ({idList})"
                    ).ToList();
                }

            }
            
            return Request.CreateResponse(HttpStatusCode.OK, cat);
        }

    }
}
