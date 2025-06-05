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
    [RoutePrefix("api/getAllJobs")]
    public class getAllJobsController : ApiController
    {
        //public HttpResponseMessage Get(int OID)
        [Route("Get")]
        public HttpResponseMessage Get(int UID, int OID)
        {
            List<JobsResponce> jobs = new List<JobsResponce>();
            List<tbl_job> tbl_Jobs = new List<tbl_job>();
            using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
            {
                string Query = "select * from tbl_job where status='A'";
                if (OID > 0)
                {
                    Query = "select * from tbl_job where status='A' AND tbl_organization_id='" + OID + "';";
                }

                tbl_Jobs = db.Database.SqlQuery<tbl_job>(Query).ToList();
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
            return Request.CreateResponse(HttpStatusCode.OK, tbl_Jobs);
        }

        [HttpGet]
        [Route("JobPreferenceCount")]
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

        [HttpGet]
        [Route("GetJobs_UserPreferences")]
        public IHttpActionResult GetJobs_UserPreferences(int UID, int OID = 0)
        {
            int PreferenceCount = 0;
            List<tbl_job> tbl_Jobs = new List<tbl_job>();
            string WhereClause = "";
            try
            {
                using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                {
                    PreferenceCount = db.Database.SqlQuery<int>("select COUNT(*) from  tbl_user_job_preferences where id_user={0} ", UID).FirstOrDefault();
                    if (PreferenceCount > 0)
                    {
                        tbl_user_job_preferences pref = new tbl_user_job_preferences();
                        List<tbl_user_job_preferences_skill> preferredSkills = new List<tbl_user_job_preferences_skill>();
                        List<tbl_user_job_preferences_location> preferredLocation = new List<tbl_user_job_preferences_location>();
                        List<tbl_user_job_industry_mapping> preferredIndustry = new List<tbl_user_job_industry_mapping>();
                        List<tbl_user_job_type_mapping> preferredJobType = new List<tbl_user_job_type_mapping>();
                        List<tbl_user_job_preferences> preferredExperience = new List<tbl_user_job_preferences>();
                        List<tbl_profile> preferredEducation = new List<tbl_profile>();

                        #region User Skills
                        preferredSkills = db.Database.SqlQuery<tbl_user_job_preferences_skill>("select * from  tbl_user_job_preferences_skill where id_user={0} ", UID).ToList();

                        if (preferredSkills.Count > 0)
                        {
                            for (int i = 0; i < preferredSkills.Count; i++)
                            {
                                if (i == 0)
                                {
                                    WhereClause = WhereClause != "" ? (WhereClause + " OR ") : WhereClause;
                                    WhereClause += " (tj.jobrequiremnt like '%" + preferredSkills[i].skill + "%'";
                                }
                                else
                                {
                                    WhereClause += " OR tj.jobrequiremnt like '%" + preferredSkills[i].skill + "%'";
                                }
                            }

                            WhereClause += ")";
                        }
                        #endregion

                        #region User Location
                        preferredLocation = db.Database.SqlQuery<tbl_user_job_preferences_location>("select * from  tbl_user_job_preferences_location where id_user={0} ", UID).ToList();

                        if (preferredLocation.Count > 0)
                        {
                            for (int i = 0; i < preferredLocation.Count; i++)
                            {
                                if (i == 0)
                                {
                                    WhereClause = WhereClause != "" ? (WhereClause + " OR ") : WhereClause;
                                    WhereClause += " (tjl.city = '" + preferredLocation[i].id_location + "'";
                                }
                                else
                                {
                                    WhereClause += " OR tjl.city = '" + preferredLocation[i].id_location + "'";
                                }
                            }
                            WhereClause += ")";
                        }
                        #endregion

                        #region User Experience

                        preferredExperience = db.Database.SqlQuery<tbl_user_job_preferences>("select * from  tbl_user_job_preferences where id_user={0} ", UID).ToList();

                        if (preferredExperience.Count > 0)
                        {

                            for (int i = 0; i < preferredExperience.Count; i++)
                            {
                                if (i == 0)
                                {
                                    WhereClause = WhereClause != "" ? (WhereClause + " OR ") : WhereClause;
                                    WhereClause += " (((tj.experiance_year*12)+tj.experiance_month) >= '" + ((preferredExperience[i].experience_years * 12) + preferredExperience[i].experience_months - 36) + "' AND ((experiance_year*12)+experiance_month)<='" + ((preferredExperience[i].experience_years * 12) + preferredExperience[i].experience_months + 36) + "'";
                                }
                                else
                                {
                                    WhereClause += " OR ((tj.experiance_year*12)+tj.experiance_month) >= '" + ((preferredExperience[i].experience_years * 12) + preferredExperience[i].experience_months - 36) + "' AND ((experiance_year*12)+experiance_month)<='" + ((preferredExperience[i].experience_years * 12) + preferredExperience[i].experience_months + 36) + "'";
                                }

                            }
                            WhereClause += ")";
                        }

                        #endregion

                        #region User Education

                        preferredEducation = db.Database.SqlQuery<tbl_profile>("select id_degree from  tbl_profile where id_user='" + UID.ToString() + "'").ToList();
                        ////var Data1 = db.Database.SqlQuery<string>("select id_degree from  tbl_profile where id_user='{0}'", UID.ToString()).FirstOrDefault();
                        if (preferredEducation.Count > 0)
                        {
                            for (int i = 0; i < preferredEducation.Count; i++)
                            {
                                if (i == 0)
                                {
                                    WhereClause = WhereClause != "" ? (WhereClause + " OR ") : WhereClause;
                                    WhereClause += " (tdm.id_degree = '" + preferredEducation[i].id_degree + "'";
                                }
                                else
                                {
                                    WhereClause += " OR tdm.id_degree = '" + preferredEducation[i].id_degree + "'";
                                }
                            }
                            WhereClause += ")";
                        }

                        #endregion

                        #region User Industry

                        preferredIndustry = db.Database.SqlQuery<tbl_user_job_industry_mapping>("select * from  tbl_user_job_industry_mapping where user_id={0} ", UID).ToList();

                        if (preferredIndustry.Count > 0)
                        {

                            for (int i = 0; i < preferredIndustry.Count; i++)
                            {
                                if (i == 0)
                                {
                                    WhereClause = WhereClause != "" ? (WhereClause + " OR ") : WhereClause;
                                    WhereClause += " (tj.id_ce_evaluation_jobindustry = '" + preferredIndustry[i].industry_id + "'";
                                }
                                else
                                {
                                    WhereClause += " OR tj.id_ce_evaluation_jobindustry = '" + preferredIndustry[i].industry_id + "'";
                                }

                            }
                            WhereClause += ")";
                        }

                        #endregion

                        #region Job Type

                        preferredJobType = db.Database.SqlQuery<tbl_user_job_type_mapping>("select * from  tbl_user_job_type_mapping where user_id={0} ", UID).ToList();

                        if (preferredJobType.Count > 0)
                        {

                            for (int i = 0; i < preferredJobType.Count; i++)
                            {
                                if (i == 0)
                                {
                                    WhereClause = WhereClause != "" ? (WhereClause + " OR ") : WhereClause;
                                    WhereClause += " (tjt.id_job_type = '" + preferredJobType[i].type_id + "'";
                                }
                                else
                                {
                                    WhereClause += " OR tjt.id_job_type = '" + preferredJobType[i].type_id + "'";
                                }

                            }
                            WhereClause += ")";
                        }

                        #endregion
                    }

                    if (WhereClause != "")
                    {
                        WhereClause = " AND (" + WhereClause + ")";

                        string Query = "select tj.* from tbl_job tj LEFT JOIN tbl_job_location_mapping tjl on tj.id_job=tjl.id_job LEFT JOIN tbl_job_type tjt on tjt.job_type=tj.jobtype LEFT JOIN tbl_degree_master tdm on tj.minquali=tdm.degree where tj.status='A'";

                        if (OID > 0)
                        {
                            Query = "select tj.* from tbl_job tj LEFT JOIN tbl_job_location_mapping tjl on tj.id_job=tjl.id_job LEFT JOIN tbl_job_type tjt on tjt.job_type=tj.jobtype LEFT JOIN tbl_degree_master tdm on tj.minquali=tdm.degree where tj.status='A' AND tbl_organization_id='" + OID + "'";
                        }

                        Query = Query + WhereClause;

                        tbl_Jobs = db.Database.SqlQuery<tbl_job>(Query).ToList();
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
            }
            catch (Exception ex)
            {
                //return InternalServerError(ex);
            }

            //return Request.CreateResponse(HttpStatusCode.OK, tbl_Jobs);
            var result = new
            {
                PreferredJobs = tbl_Jobs
            };

            return Ok(result);
        }
    }
}
