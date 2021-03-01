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
    
    public partial class MOUPartnerScope
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MOUPartnerScope()
        {
            this.ActivityPartners = new HashSet<ActivityPartner>();
            this.AcademicCollaborations = new HashSet<AcademicCollaboration>();
            this.MOAPartnerScopes = new HashSet<MOAPartnerScope>();
            this.ResearchPartners = new HashSet<ResearchPartner>();
        }
    
        public int mou_partner_scope_id { get; set; }
        public Nullable<int> mou_id { get; set; }
        public int partner_id { get; set; }
        public int scope_id { get; set; }
        public Nullable<int> mou_bonus_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityPartner> ActivityPartners { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcademicCollaboration> AcademicCollaborations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOAPartnerScope> MOAPartnerScopes { get; set; }
        public virtual MOU MOU { get; set; }
        public virtual MOUBonu MOUBonu { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual CollaborationScope CollaborationScope { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearchPartner> ResearchPartners { get; set; }
    }
}
