using m2ostnextservice.Models;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;

public class tbl_job
{
    public int id_job {  get; set; }
    public string compname {  get; set; }
    public int id_ce_evaluation_jobindustry { get; set; }
    public int jobcategory { get; set; }
    public string jobtitle { get; set; }
    public string minquali { get; set; }
    public string jobdesc { get; set; }
    public string jobrequiremnt { get; set; }
    public string gender { get; set; }
    public string experiance_month {  get; set; }
    public string experiance_year { get; set; }
    public int noofopen { get; set; }
    public int jobpoints { get; set; }
    public int minsalary {  get; set; }
    public int maxsalary { get; set; }
    public int applyflag { get; set; }
    public List<string> loc = new List<string>();
}

public class tbl_interview_rounds_mapping
{
    public int? row_id { get; set; }

    public string interview_type_name { get; set; }

    public string round_name { get; set; }

    public string round_number { get; set; }

    public int? interview_id { get; set; }

    public string booktypestatus { get; set; }

    public int? id_job { get; set; }
}
public class tbl_job_publish_calender_report
{
    public int? publish_id { get; set; }

    public int? id_job { get; set; }

    public int? round_number { get; set; }

    public int? interview_id { get; set; }

    public string publishDate { get; set; }

    public string publishFromTime { get; set; }

    public string publishToTime { get; set; }

    public string publishSlots { get; set; }

    public string publishStatus { get; set; }

    public int? publishInterval { get; set; }

    public string booktypestatus { get; set; }

    public string isDisable { get; set; }

    public DateTime updated_datetime { get; set; }
}

public class tbl_job_port
{
    public int id_job { get; set; }

    public int id_organization { get; set; }

    public string compname { get; set; }

    public string phone { get; set; }

    public int jobcategory { get; set; }

    public string jobtitle { get; set; }

    public int minsalary { get; set; }

    public int maxsalary { get; set; }

    public int noofopen { get; set; }

    public string gender { get; set; }

    public string minquali { get; set; }

    public string englishreq { get; set; }

    public string experience { get; set; }

    public string jobdesc { get; set; }

    public string jobrequiremnt { get; set; }

    public string jobtiming { get; set; }

    public string shift { get; set; }

    public string name { get; set; }

    public string mailid { get; set; }

    public string state { get; set; }

    public string city { get; set; }

    public string area { get; set; }

    public DateTime expdate { get; set; }

    public string jobtype { get; set; }

    public string status { get; set; }

    public string save_type { get; set; }

    public DateTime updated_datetime { get; set; }

    public int jobpoints { get; set; }

    public string desc_name { get; set; }

    public int desc_status { get; set; }

    public int AppliedCount { get; set; }

    public string state_name { get; set; }

    public string city_name { get; set; }

    public int? experiance_month { get; set; }

    public int? experiance_year { get; set; }

    public string total_experiance { get; set; }

    public string pdf_cv { get; set; }

    public string video_cv { get; set; }

    public string cv_not_required { get; set; }

    public string is_interview { get; set; }

    public bool? skillminifest_status { get; set; }

    public int hurryflag { get; set; }

    public int sortid { get; set; }

    public int? id_ce_evaluation_jobindustry { get; set; }

    public int attempt_no { get; set; }

    public string ce_industry_role { get; set; }

    public List<tbl_job_location_mapping> Cityidlist { get; set; }
}
public class tbl_job_user_log
{
    public int id_job_log { get; set; }

    public int id_user { get; set; }

    public int id_job { get; set; }

    public string status { get; set; }

    public string rejectedby { get; set; }

    public DateTime updated_date_time { get; set; }

    public int? id_org { get; set; }

    public int id_organization { get; set; }

    public string compname { get; set; }

    public string phone { get; set; }

    public string jobcategory { get; set; }

    public string jobtitle { get; set; }

    public int minsalary { get; set; }

    public int maxsalary { get; set; }

    public int noofopen { get; set; }

    public string gender { get; set; }

    public string minquali { get; set; }

    public string englishreq { get; set; }

    public string experience { get; set; }

    public string jobdesc { get; set; }

    public string jobrequiremnt { get; set; }

    public string jobtiming { get; set; }

    public string shift { get; set; }

    public string name { get; set; }

    public string mailid { get; set; }

    public string state { get; set; }

    public string city { get; set; }

    public string area { get; set; }

    public DateTime expdate { get; set; }

    public string jobtype { get; set; }

    public string save_type { get; set; }

    public int jobpoints { get; set; }

    public string CityName { get; set; }

    public List<tbl_job_location_mapping> Cityidlist { get; set; }

    public int AppliedCount { get; set; }

    public tbl_job_assessment_mapping assessment { get; set; }

    public string total_experience { get; set; }

    public tbl_ce_career_evaluation_master careermaster { get; set; }

    public int attempt_no { get; set; }

    public string city_name { get; set; }

    public List<string> citylist { get; set; }

    public tbl_job_location_mapping tbl_job_location_mapping { get; set; }

    public tbl_min_qualification_mapping MinimumQualification { get; set; }

    public string skillminifest_status { get; set; }
}
public class tbl_job_location_mapping
{
    public int id_location_mapping { get; set; }

    public int id_job { get; set; }

    public int city { get; set; }

    public int cityidlist { get; set; }

    public string cityname { get; set; }
}
public class tbl_job_assessment_mapping
{
    public int assessment_mapping_id { get; set; }

    public int id_job { get; set; }

    public string round_number { get; set; }

    public string round_name { get; set; }

    public int interview_id { get; set; }

    public string interview_type_name { get; set; }

    public int id_ce_career_evaluation_master { get; set; }

    public string career_evaluation_code { get; set; }

    public int set_minimum_point { get; set; }

    public DateTime updated_datetime { get; set; }
}
public class tbl_min_qualification_mapping
{
    public int id_qualification { get; set; }

    public int id_job { get; set; }

    public string minquali { get; set; }
}