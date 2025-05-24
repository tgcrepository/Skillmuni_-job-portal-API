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
    public class getStateController : ApiController
    {
        public HttpResponseMessage Get([FromUri] int[] CountryId)
        {
            List<cities> cat = new List<cities>();

            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                if (CountryId == null || CountryId.Length == 0)
                {
                    cat = db.Database.SqlQuery<cities>("SELECT * FROM states WHERE status='A'").ToList();
                }
                else
                {
                    
                    string idList = string.Join(",", CountryId);
                    cat = db.Database.SqlQuery<cities>(
                        $"SELECT * FROM states WHERE country_id IN ({idList})"
                    ).ToList();
                }

            }
            
            return Request.CreateResponse(HttpStatusCode.OK, cat);
        }

    }
}
