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
    
    public partial class Procedure
    {
        public int procedure_id { get; set; }
        public string procedure_name { get; set; }
        public int collab_id { get; set; }
        public int account_id { get; set; }
        public Nullable<int> article_id { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual AcademicCollaboration AcademicCollaboration { get; set; }
        public virtual News News { get; set; }
    }
}
