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
    
    public partial class RequestPaper
    {
        public int request_id { get; set; }
        public Nullable<int> specialization_id { get; set; }
        public string type { get; set; }
        public string reward_type { get; set; }
        public int status_id { get; set; }
        public string total_reward { get; set; }
        public int paper_id { get; set; }
    
        public virtual Specialization Specialization { get; set; }
        public virtual BaseRequest BaseRequest { get; set; }
        public virtual Paper Paper { get; set; }
        public virtual PaperStatu PaperStatu { get; set; }
    }
}
