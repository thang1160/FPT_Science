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
    
    public partial class MOU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MOU()
        {
            this.MOAs = new HashSet<MOA>();
            this.MOUBonus = new HashSet<MOUBonu>();
            this.MOUPartners = new HashSet<MOUPartner>();
            this.MOUPartnerScopes = new HashSet<MOUPartnerScope>();
            this.MOUStatusHistories = new HashSet<MOUStatusHistory>();
        }
    
        public int mou_id { get; set; }
        public string mou_code { get; set; }
        public Nullable<System.DateTime> mou_end_date { get; set; }
        public string mou_note { get; set; }
        public string evidence { get; set; }
        public Nullable<int> office_id { get; set; }
        public int account_id { get; set; }
        public System.DateTime add_time { get; set; }
        public bool is_deleted { get; set; }
        public int noti_count { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOA> MOAs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOUBonu> MOUBonus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOUPartner> MOUPartners { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOUPartnerScope> MOUPartnerScopes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOUStatusHistory> MOUStatusHistories { get; set; }
    }
}
