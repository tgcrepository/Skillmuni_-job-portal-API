//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace m2ostnextservice
{
    using System;
    using System.Collections.Generic;
    
    public partial class sc_program_content_summary
    {
        public int id_sc_program_content_summary { get; set; }
        public Nullable<int> id_category { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> id_organization { get; set; }
        public Nullable<int> totoal_count { get; set; }
        public Nullable<int> completed_count { get; set; }
        public Nullable<double> percentage { get; set; }
        public Nullable<double> content_weightage { get; set; }
        public Nullable<System.DateTime> log_datetime { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
    }
}
