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
    
    public partial class Profile
    {
        public int people_id { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> birth_date { get; set; }
        public string nationality { get; set; }
        public string working_address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string google_scholar { get; set; }
        public string cv { get; set; }
        public string research_area { get; set; }
        public Nullable<int> avatar_id { get; set; }
    
        public virtual File File { get; set; }
        public virtual Person Person { get; set; }
    }
}
