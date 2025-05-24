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
    public class getJobPreferencesDetailsController : ApiController
    {
        public HttpResponseMessage Get(int UID)
        {
            preferencemodel result = new preferencemodel();

            try
            {

                using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                {

                    tbl_user_job_preferences pref = new tbl_user_job_preferences();
                    pref = db.Database.SqlQuery<tbl_user_job_preferences>("select * from  tbl_user_job_preferences where id_user={0} ", UID).FirstOrDefault();
                    if (pref != null)
                    {
                        result.experience_months = pref.experience_months;
                        result.experience_years = pref.experience_years;
                    }
                    List< tbl_user_job_preferences_skill > SKILLLIST = db.Database.SqlQuery<tbl_user_job_preferences_skill>("select * from  tbl_user_job_preferences_skill where id_user={0}", UID).ToList();
                    int i = 1;
                    int sklcnt = SKILLLIST.Count;
                    foreach (var itm in SKILLLIST)
                    {
                        result.skills = result.skills + itm.skill;
                        if (i < sklcnt)
                        {
                            result.skills = result.skills + ",";
                        }
                        i++;
                    }
                    result.id_category = db.Database.SqlQuery<int>(
                            "SELECT id_category FROM tbl_user_job_preferences_category WHERE id_user = {0}", UID).ToList();

                    result.id_city = db.Database.SqlQuery<int>(
                        "SELECT id_location FROM tbl_user_job_preferences_location WHERE id_user = {0}", UID).ToList();

                    string cidList = string.Join(",", result.id_city);
                    result.id_state = db.Database.SqlQuery<int>(
                        $"SELECT DISTINCT state_id FROM cities WHERE id IN ({cidList})", UID).ToList();

                    string sidList = string.Join(",", result.id_state);
                    result.id_country = db.Database.SqlQuery<int>(
                        $"SELECT DISTINCT country_id FROM states WHERE id IN ({sidList})", UID).ToList();

                    result.id_job_type = db.Database.SqlQuery<int>(
                        "SELECT type_id FROM tbl_user_job_type_mapping WHERE user_id = {0}", UID).ToList();

                    result.id_industry = db.Database.SqlQuery<int>(
                        "SELECT industry_id FROM tbl_user_job_industry_mapping WHERE user_id = {0}", UID).ToList();

                    result.id_job_role = db.Database.SqlQuery<int>(
                        "SELECT role_id FROM tbl_user_job_role_mapping WHERE user_id = {0}", UID).ToList();

                 
                

                    int resumeflag=  db.Database.SqlQuery<int>("select ResumeFlag from  tbl_profile  where ID_USER={0} ",UID).FirstOrDefault();
                    if (resumeflag == 1)
                    {
                        result.resumepath= ConfigurationManager.AppSettings["ResumePath"].ToString()+"/"+ db.Database.SqlQuery<string>("select ResumeLocation from  tbl_profile  where ID_USER={0} ", UID).FirstOrDefault();
                    }


                    tbl_cv_master mas = db.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", UID, 1).FirstOrDefault();
                    if (mas != null)
                    {
                        result.isVideoCvPresent = 1;
                        tbl_video_cv vidcv= db.Database.SqlQuery<tbl_video_cv>(" select * from tbl_video_cv where id_cv ={0}", mas.id_cv).FirstOrDefault();
                        result.VideoCVStatus = vidcv.status;
                        result.VideoCVLink= ConfigurationManager.AppSettings["VidResumePath"].ToString()+"/"+ UID  + vidcv.extn;

                    }
                    else
                    {
                        result.isVideoCvPresent = 0;
                    }
                    tbl_cv_master mascv = db.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", UID, 2).FirstOrDefault();
                    if (mascv.isUploaded != null)
                    {
                        result.isClassicCVPresent = 1;
                       
                        result.ClassicCvLink = ConfigurationManager.AppSettings["ClassicResume"] + "/" + UID + ".pdf";

                    }
                    else
                    {
                        result.isClassicCVPresent = 0;
                    }


                }



            }
            catch (Exception e)
            {
                throw e;
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
