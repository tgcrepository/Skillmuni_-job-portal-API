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
    
    public partial class tbl_task
    {
        public int id_task { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> id_org { get; set; }
        public string task { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public string status { get; set; }
    }
}
