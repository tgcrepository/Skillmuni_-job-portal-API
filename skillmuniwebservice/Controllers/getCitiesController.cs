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
    public class getCitiesController : ApiController
    {
        public HttpResponseMessage Get([FromUri] int[] StateID)
        {
            List<cities> cat = new List<cities>();

            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                if (StateID == null || StateID.Length == 0)
                {
                    cat = db.Database.SqlQuery<cities>("SELECT * FROM cities WHERE status='A'").ToList();
                }
                else
                {
                    
                    string idList = string.Join(",", StateID);
                    cat = db.Database.SqlQuery<cities>(
                        $"SELECT * FROM cities WHERE status = 'A' AND state_id IN ({idList})"
                    ).ToList();
                }

            }
            
            return Request.CreateResponse(HttpStatusCode.OK, cat);
        }

    }
}
