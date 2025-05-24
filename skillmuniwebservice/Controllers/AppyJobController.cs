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
    public class AppyJobController : ApiController
    {

        [HttpPost]
        [Route("api/applyjob")]
        public IHttpActionResult ApplyJobAction(int UID, int OID, int id_job)
        {
            try
            {
                using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                {
                    // Log the job application
                    db.Database.ExecuteSqlCommand("INSERT INTO tbl_job_user_log (id_user, id_job, status, updated_date_time, id_org) VALUES ({0}, {1}, {2}, {3}, {4})", UID, id_job, "A", DateTime.Now, OID);

                    // Fetch the first interview round details
                    tbl_interview_rounds_mapping interviewRoundsMapping2 = db.Database.SqlQuery<tbl_interview_rounds_mapping>(
                        @"SELECT booktypestatus, interview_id, interview_type_name, round_number, id_job, round_name 
                      FROM tbl_job_assessment_mapping WHERE id_job = {0} 
                      UNION 
                      SELECT booktypestatus, interview_id, interview_type_name, round_number, id_job, round_name 
                      FROM tbl_job_online_mapping WHERE id_job = {1} 
                      UNION 
                      SELECT booktypestatus, interview_id, interview_type_name, round_number, id_job, round_name 
                      FROM tbl_job_offline_mapping WHERE id_job = {2} 
                      UNION 
                      SELECT booktypestatus, interview_id, interview_type_name, round_number, id_job, round_name 
                      FROM tbl_job_telephonic_mapping WHERE id_job = {3} 
                      ORDER BY round_number",
                        id_job, id_job, id_job, id_job).FirstOrDefault();

                    if (interviewRoundsMapping2 != null)
                    {
                        tbl_job_publish_calender_report publishCalenderReport2 = db.Database.SqlQuery<tbl_job_publish_calender_report>(
                            @"SELECT * FROM tbl_job_publish_calender_report 
                          WHERE id_job = {0} AND interview_id = {1} AND round_number = {2} AND publishSlots IS NOT NULL",
                            id_job, interviewRoundsMapping2.interview_id, interviewRoundsMapping2.round_number).FirstOrDefault();

                        int? interviewId = interviewRoundsMapping2.interview_id;

                        // Round 1
                        if (interviewId.GetValueOrDefault() == 1 && interviewId.HasValue)
                        {
                            db.Database.ExecuteSqlCommand("INSERT INTO tbl_job_interview_result_log (id_job, id_user, interview_id, interview_status, round_number, updated_datetime, interview_type_name, assessmentstatus, round_name) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                                id_job, UID, interviewRoundsMapping2.interview_id, "N", interviewRoundsMapping2.round_number, DateTime.Now, interviewRoundsMapping2.interview_type_name, "P", interviewRoundsMapping2.round_name);
                        }

                        // Round 2
                        if (interviewId.GetValueOrDefault() == 2 && interviewId.HasValue)
                        {
                            if (interviewRoundsMapping2.booktypestatus == "publish" && publishCalenderReport2 != null)
                            {
                                db.Database.ExecuteSqlCommand("INSERT INTO tbl_job_interview_result_log (id_job, id_user, interview_id, interview_status, round_number, updated_datetime, interview_type_name, publishstatus, round_name) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                                    id_job, UID, interviewRoundsMapping2.interview_id, "N", interviewRoundsMapping2.round_number, DateTime.Now, interviewRoundsMapping2.interview_type_name, "P", interviewRoundsMapping2.round_name);
                            }
                            else
                            {
                                db.Database.ExecuteSqlCommand("INSERT INTO tbl_job_interview_result_log (id_job, id_user, interview_id, interview_status, round_number, updated_datetime, interview_type_name, round_name) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                                    id_job, UID, interviewRoundsMapping2.interview_id, "N", interviewRoundsMapping2.round_number, DateTime.Now, interviewRoundsMapping2.interview_type_name, interviewRoundsMapping2.round_name);
                            }
                        }

                        // Round 3
                        if (interviewId.GetValueOrDefault() == 3 && interviewId.HasValue)
                        {
                            if (interviewRoundsMapping2.booktypestatus == "publish" && publishCalenderReport2 != null)
                            {
                                db.Database.ExecuteSqlCommand("INSERT INTO tbl_job_interview_result_log (id_job, id_user, interview_id, interview_status, round_number, updated_datetime, interview_type_name, publishstatus, round_name) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                                    id_job, UID, interviewRoundsMapping2.interview_id, "N", interviewRoundsMapping2.round_number, DateTime.Now, interviewRoundsMapping2.interview_type_name, "P", interviewRoundsMapping2.round_name);
                            }
                            else
                            {
                                db.Database.ExecuteSqlCommand("INSERT INTO tbl_job_interview_result_log (id_job, id_user, interview_id, interview_status, round_number, updated_datetime, interview_type_name, round_name) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                                    id_job, UID, interviewRoundsMapping2.interview_id, "N", interviewRoundsMapping2.round_number, DateTime.Now, interviewRoundsMapping2.interview_type_name, interviewRoundsMapping2.round_name);
                            }
                        }

                        // Round 4
                        if (interviewId.GetValueOrDefault() == 4 && interviewId.HasValue)
                        {
                            if (interviewRoundsMapping2.booktypestatus == "publish" && publishCalenderReport2 != null)
                            {
                                db.Database.ExecuteSqlCommand("INSERT INTO tbl_job_interview_result_log (id_job, id_user, interview_id, interview_status, round_number, updated_datetime, interview_type_name, publishstatus, round_name) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                                    id_job, UID, interviewRoundsMapping2.interview_id, "N", interviewRoundsMapping2.round_number, DateTime.Now, interviewRoundsMapping2.interview_type_name, "P", interviewRoundsMapping2.round_name);
                            }
                            else
                            {
                                db.Database.ExecuteSqlCommand("INSERT INTO tbl_job_interview_result_log (id_job, id_user, interview_id, interview_status, round_number, updated_datetime, interview_type_name, round_name) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                                    id_job, UID, interviewRoundsMapping2.interview_id, "N", interviewRoundsMapping2.round_number, DateTime.Now, interviewRoundsMapping2.interview_type_name, interviewRoundsMapping2.round_name);
                            }
                        }
                    }
                }
                return Ok("success");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("api/getresume")]
        public IHttpActionResult GetResumeDetails(int UID, int OID)
        {
            try
            {
                int resumeFlag = 0;
                string resumeLocation = "";

                using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                {
                    resumeFlag = db.Database.SqlQuery<int>(
                        "SELECT ResumeFlag FROM tbl_profile WHERE ID_USER = {0}", UID).FirstOrDefault();

                    if (resumeFlag == 1)
                    {
                        resumeLocation = db.Database.SqlQuery<string>(
                            "SELECT ResumeLocation FROM tbl_profile WHERE ID_USER = {0}", UID).FirstOrDefault();
                    }
                }
                string resumePath = ConfigurationManager.AppSettings["resumepath"] +"/" +resumeLocation;
                return Ok(new
                {
                    resumeflag = resumeFlag,
                    resumename = resumeLocation,
                    resumelink = resumePath
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/uploadresume")]
        public IHttpActionResult UploadResumeAction(int UID, int OID)
        {
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files["resumepath"];
                    string extension = Path.GetExtension(file.FileName);

                    if (file != null)
                    {
                        string resumeDirectory = HttpContext.Current.Server.MapPath("~/Content/Resume");
                        if (!Directory.Exists(resumeDirectory))
                        {
                            Directory.CreateDirectory(resumeDirectory);
                        }

                        string filename = Path.Combine(resumeDirectory, UID.ToString() + extension);
                        file.SaveAs(filename);

                        string resumeLocation = UID.ToString() + extension;
                        using (m2ostnextserviceDbContext db = new m2ostnextserviceDbContext())
                        {
                            db.Database.ExecuteSqlCommand(
                                "UPDATE tbl_profile SET ResumeLocation = {0}, ResumeFlag = {1} WHERE ID_USER = {2}",
                                resumeLocation, 1, UID);
                        }

                        return Ok(new { message = "Resume uploaded successfully", resumeLocation });
                    }
                    else
                    {
                        return BadRequest("No file found in the request.");
                    }
                }
                else
                {
                    return BadRequest("No files received.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
