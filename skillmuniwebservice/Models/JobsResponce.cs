using Org.BouncyCastle.Bcpg;
using System.Collections.Generic;

public class JobsResponce
{
    public int id_job { get; set; }
    public string compname { get; set; }
    public int id_ce_evaluation_jobindustry { get; set; }
    public int jobcategory { get; set; }
    public string jobtitle { get; set; }
    public string minquali { get; set; }
    public string jobdesc { get; set; }
    public string jobrequiremnt { get; set; }
    public string gender { get; set; }
    public string experiance_month { get; set; }
    public string experiance_year { get; set; }
    public int noofopen { get; set; }
    public int jobpoints { get; set; }
    public int minsalary { get; set; }
    public int maxsalary { get; set; }
    List<string> loc {  get; set; } 

}

