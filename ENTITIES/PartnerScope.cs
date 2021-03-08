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
    
    public partial class PartnerScope
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PartnerScope()
        {
            this.AcademicCollaborations = new HashSet<AcademicCollaboration>();
            this.MOAPartnerScopes = new HashSet<MOAPartnerScope>();
            this.MOUPartnerScopes = new HashSet<MOUPartnerScope>();
            this.ActivityPartners = new HashSet<ActivityPartner>();
            this.ResearchPartners = new HashSet<ResearchPartner>();
        }
    
        public int partner_scope_id { get; set; }
        public int partner_id { get; set; }
        public int scope_id { get; set; }
        public int reference_count { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcademicCollaboration> AcademicCollaborations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOAPartnerScope> MOAPartnerScopes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOUPartnerScope> MOUPartnerScopes { get; set; }
        public virtual Partner Partner { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityPartner> ActivityPartners { get; set; }
        public virtual CollaborationScope CollaborationScope { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearchPartner> ResearchPartners { get; set; }
    }
}
