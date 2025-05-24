using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m2ostnextservice.Models;

namespace m2ostnextservice.Controllers
{
    public class getIndustryListController : ApiController
    {

        public HttpResponseMessage Get()
        {
            List<tbl_job_industry> mas = new List<tbl_job_industry>();
            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                mas = db.Database.SqlQuery<tbl_job_industry>("select * from tbl_job_industry where status='A' ").ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, mas);

        }
    }
}
