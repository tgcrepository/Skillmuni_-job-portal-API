using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m2ostnextservice.Models;

namespace m2ostnextservice.Controllers
{
    public class getJobRoleListController : ApiController
    {
        public HttpResponseMessage Get([FromUri] int[] CategoryID)
        {
            List<tbl_job_role> mas = new List<tbl_job_role>();
            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                if (CategoryID == null || CategoryID.Length == 0)
                {
                    // Return all job categories if IndustryID is null or empty
                    mas = db.Database.SqlQuery<tbl_job_role>("SELECT * FROM tbl_job_role WHERE status='A'").ToList();
                }
                else
                {
                    // Filter job categories based on IndustryID array
                    string idList = string.Join(",", CategoryID);
                    mas = db.Database.SqlQuery<tbl_job_role>(
                        $"SELECT * FROM tbl_job_role WHERE status='A' AND id_category IN ({idList})"
                    ).ToList();
                }

            }

            return Request.CreateResponse(HttpStatusCode.OK, mas);

        }

    }
}
