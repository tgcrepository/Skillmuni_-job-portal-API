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
    
    public partial class tbl_notification_pre_config
    {
        public int id_notification_config { get; set; }
        public Nullable<int> id_notification { get; set; }
        public Nullable<int> id_creater { get; set; }
        public string notification_key { get; set; }
        public string notification_action_type { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> id_content { get; set; }
        public Nullable<int> id_category { get; set; }
        public Nullable<int> id_assessment { get; set; }
        public string user_type { get; set; }
        public Nullable<System.DateTime> read_timestamp { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
    }
}
