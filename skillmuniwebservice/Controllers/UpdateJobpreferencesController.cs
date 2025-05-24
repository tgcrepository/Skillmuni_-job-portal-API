using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using m2ostnextservice.Models;
using Newtonsoft.Json;

namespace m2ostnextservice.Controllers
{
    public class UpdateJobpreferencesController : ApiController
    {

        public HttpResponseMessage Post([FromBody] preferencemodelrequest obj)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            string response = "";
            try
            {

                using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                {
                    tbl_user_job_preferences pref = new tbl_user_job_preferences();
                    pref = db.Database.SqlQuery<tbl_user_job_preferences>("select * from  tbl_user_job_preferences where id_user={0} ", obj.id_user).FirstOrDefault();

                    if (pref == null)
                    {


                        db.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences (experience_years,experience_months,status,updated_date_time,id_user) values({0},{1},{2},{3},{4})", obj.experience_years, obj.experience_months, "A", DateTime.Now, obj.id_user);
                        string[] skill = obj.skills.Split(',');
                       
                        foreach (var itm in skill)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_skill (id_user,skill,status,updated_date_time) values({0},{1},{2},{3})", obj.id_user, itm, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_category)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_category (id_category,id_user,status,updated_date_time) values({0},{1},{2},{3})", itm, obj.id_user, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_city)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_location (id_location,id_user,status,updated_date_time) values({0},{1},{2},{3})", itm, obj.id_user, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_job_type)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_type_mapping (user_id,type_id,status,CreatedAt) values({0},{1},{2},{3})", obj.id_user, itm, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_industry)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_industry_mapping (industry_id, user_id, status, CreatedAt) values({0},{1},{2},{3})", itm, obj.id_user, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_job_role)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_role_mapping (role_id, user_id, status, CreatedAt) values({0},{1},{2},{3})", itm, obj.id_user, "A", DateTime.Now);

                        }




                    }
                    else
                    {
                        db.Database.ExecuteSqlCommand("update  tbl_user_job_preferences set experience_years={0},experience_months={1},status={2},updated_date_time={3} where id_user={4}", obj.experience_years, obj.experience_months, "A", DateTime.Now, obj.id_user);
                        //db.Database.ExecuteSqlCommand("delete from  tbl_user_job_preferences_skill where id_user={0}", obj.id_user);
                        //db.Database.ExecuteSqlCommand("delete from  tbl_user_job_preferences_category where id_user={0}", obj.id_user);
                        //db.Database.ExecuteSqlCommand("delete from  tbl_user_job_preferences_location where id_user={0}", obj.id_user);
                        //db.Database.ExecuteSqlCommand("delete from  tbl_user_job_type_mapping where id_user={0}", obj.id_user);
                        //db.Database.ExecuteSqlCommand("delete from  tbl_user_job_industry_mapping where id_user={0}", obj.id_user);
                        //db.Database.ExecuteSqlCommand("delete from  tbl_user_job_type_mapping where id_user={0}", obj.id_user);
                       
                        // Delete from tbl_user_job_preferences_skill if records exist
                        int skillCount = db.Database.SqlQuery<int>(
                            "SELECT COUNT(*) FROM tbl_user_job_preferences_skill WHERE id_user = {0}", obj.id_user
                        ).FirstOrDefault();

                        if (skillCount > 0)
                        {
                            db.Database.ExecuteSqlCommand("DELETE FROM tbl_user_job_preferences_skill WHERE id_user = {0}", obj.id_user);
                        }

                        // Delete from tbl_user_job_preferences_category if records exist
                        int categoryCount = db.Database.SqlQuery<int>(
                            "SELECT COUNT(*) FROM tbl_user_job_preferences_category WHERE id_user = {0}", obj.id_user
                        ).FirstOrDefault();

                        if (categoryCount > 0)
                        {
                            db.Database.ExecuteSqlCommand("DELETE FROM tbl_user_job_preferences_category WHERE id_user = {0}", obj.id_user);
                        }

                        // Delete from tbl_user_job_preferences_location if records exist
                        int locationCount = db.Database.SqlQuery<int>(
                            "SELECT COUNT(*) FROM tbl_user_job_preferences_location WHERE id_user = {0}", obj.id_user
                        ).FirstOrDefault();

                        if (locationCount > 0)
                        {
                            db.Database.ExecuteSqlCommand("DELETE FROM tbl_user_job_preferences_location WHERE id_user = {0}", obj.id_user);
                        }

                        // Delete from tbl_user_job_type_mapping if records exist
                        int jobTypeCount = db.Database.SqlQuery<int>(
                            "SELECT COUNT(*) FROM tbl_user_job_type_mapping WHERE user_id = {0}", obj.id_user
                        ).FirstOrDefault();

                        if (jobTypeCount > 0)
                        {
                            db.Database.ExecuteSqlCommand("DELETE FROM tbl_user_job_type_mapping WHERE user_id = {0}", obj.id_user);
                        }

                        // Delete from tbl_user_job_industry_mapping if records exist
                        int industryCount = db.Database.SqlQuery<int>(
                            "SELECT COUNT(*) FROM tbl_user_job_industry_mapping WHERE user_id = {0}", obj.id_user
                        ).FirstOrDefault();

                        if (industryCount > 0)
                        {
                            db.Database.ExecuteSqlCommand("DELETE FROM tbl_user_job_industry_mapping WHERE user_id = {0}", obj.id_user);
                        }
                        // Delete from tbl_user_job_type_mapping if records exist
                        int jobRoleCount = db.Database.SqlQuery<int>(
                            "SELECT COUNT(*) FROM tbl_user_job_role_mapping WHERE user_id = {0}", obj.id_user
                        ).FirstOrDefault();

                        if (jobRoleCount > 0)
                        {
                            db.Database.ExecuteSqlCommand("DELETE FROM tbl_user_job_role_mapping WHERE user_id = {0}", obj.id_user);
                        }






                        string[] skill = obj.skills.Split(',');


                        foreach (var itm in skill)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_skill (id_user,skill,status,updated_date_time) values({0},{1},{2},{3})", obj.id_user, itm, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_category)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_category (id_category,id_user,status,updated_date_time) values({0},{1},{2},{3})", itm, obj.id_user, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_city)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_location (id_location,id_user,status,updated_date_time) values({0},{1},{2},{3})", itm, obj.id_user, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_job_type)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_type_mapping (user_id,type_id,status,CreatedAt) values({0},{1},{2},{3})", obj.id_user, itm, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_industry)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_industry_mapping (industry_id, user_id, status, CreatedAt) values({0},{1},{2},{3})", itm, obj.id_user, "A", DateTime.Now);

                        }
                        foreach (var itm in obj.id_job_role)
                        {

                            db.Database.ExecuteSqlCommand("insert into tbl_user_job_role_mapping (role_id, user_id, status, CreatedAt) values({0},{1},{2},{3})", itm, obj.id_user, "A", DateTime.Now);

                        }
                    }






                }

                response = "SUCCESS";
            }
            catch (Exception e)
            {
                new Utility().eventLog(controllerName + " : " + e.Message);
                new Utility().eventLog("Inner Exeption" + " : " + e.InnerException.ToString());
                new Utility().eventLog("Additional Details" + " : " + e.Message);
                response = "FAILURE";
            }
            finally
            {
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }
}
