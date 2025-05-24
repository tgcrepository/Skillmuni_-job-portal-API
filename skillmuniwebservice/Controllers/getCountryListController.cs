using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using m2ostnextservice.Models;

namespace m2ostnextservice.Controllers
{
    public class getCountryListController : ApiController
    {

        public HttpResponseMessage Get()
        {
            List<tbl_contries> mas = new List<tbl_contries>();
            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                mas = db.Database.SqlQuery<tbl_contries>("select * from countries where status='A' ").ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, mas);

        }
    }
}
