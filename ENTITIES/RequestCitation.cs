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
    
    public partial class RequestCitation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequestCitation()
        {
            this.Citations = new HashSet<Citation>();
            this.Decisions = new HashSet<Decision>();
        }
    
        public int request_id { get; set; }
        public int status_id { get; set; }
        public int people_id { get; set; }
        public string current_mssv_msnv { get; set; }
        public System.DateTime date_request { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual PaperStatu PaperStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Citation> Citations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Decision> Decisions { get; set; }
    }
}
