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
    
    public partial class tbl_csst_role
    {
        public int id_csst_role { get; set; }
        public string csst_role { get; set; }
        public Nullable<int> id_organization { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_dated_time { get; set; }
    
        public virtual tbl_organization tbl_organization { get; set; }
    }
}
