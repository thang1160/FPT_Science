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
    
    public partial class Author
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Author()
        {
            this.RequestCitations = new HashSet<RequestCitation>();
            this.AuthorInventions = new HashSet<AuthorInvention>();
            this.AuthorPapers = new HashSet<AuthorPaper>();
            this.RequestPapers = new HashSet<RequestPaper>();
        }
    
        public int people_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public Nullable<long> bank_number { get; set; }
        public string bank_branch { get; set; }
        public Nullable<long> tax_code { get; set; }
        public string identification_number { get; set; }
        public string mssv_msnv { get; set; }
        public Nullable<bool> is_reseacher { get; set; }
        public Nullable<int> identification_file_id { get; set; }
        public Nullable<int> office_id { get; set; }
        public Nullable<int> contract_id { get; set; }
        public Nullable<int> title_id { get; set; }
    
        public virtual File File { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestCitation> RequestCitations { get; set; }
        public virtual ContractType ContractType { get; set; }
        public virtual Title Title { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthorInvention> AuthorInventions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthorPaper> AuthorPapers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestPaper> RequestPapers { get; set; }
    }
}
