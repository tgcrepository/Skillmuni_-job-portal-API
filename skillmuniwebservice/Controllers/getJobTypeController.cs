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
    public class getJobTypeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            List<tbl_job_type> cat = new List<tbl_job_type>();

            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                    cat = db.Database.SqlQuery<tbl_job_type>("SELECT * FROM tbl_job_type WHERE status='A'").ToList();
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, cat);
        }

    }
}
