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
    
    public partial class AcademicCollaboration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicCollaboration()
        {
            this.CollaborationStatusHistories = new HashSet<CollaborationStatusHistory>();
            this.Procedures = new HashSet<Procedure>();
        }
    
        public System.DateTime plan_study_start_date { get; set; }
        public System.DateTime plan_study_end_date { get; set; }
        public string note { get; set; }
        public int collab_id { get; set; }
        public int direction_id { get; set; }
        public int people_id { get; set; }
        public Nullable<bool> is_supported { get; set; }
        public Nullable<int> mou_partner_scope_id { get; set; }
        public int collab_status_type_id { get; set; }
        public Nullable<System.DateTime> actual_study_start_date { get; set; }
        public Nullable<System.DateTime> actual_study_end_date { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Direction Direction { get; set; }
        public virtual MOUPartnerScope MOUPartnerScope { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollaborationStatusHistory> CollaborationStatusHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Procedure> Procedures { get; set; }
        public virtual AcademicCollaborationStatusType AcademicCollaborationStatusType { get; set; }
    }
}
