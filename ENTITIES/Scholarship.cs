//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ENTITIES
{
    using System;
    using System.Collections.Generic;
    
    public partial class Scholarship
    {
        public int scholarship_id { get; set; }
        public string scholarship_title { get; set; }
        public Nullable<System.DateTime> scholarship_start_date { get; set; }
        public Nullable<System.DateTime> scholarship_end_date { get; set; }
        public Nullable<int> country_id { get; set; }
        public int scholarship_status_id { get; set; }
        public int specialization_id { get; set; }
        public Nullable<int> article_id { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ScholarshipStatu ScholarshipStatu { get; set; }
        public virtual News News { get; set; }
    }
}
