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
    
    public partial class CitationStatusLanguage
    {
        public int citation_status_id { get; set; }
        public int language_id { get; set; }
        public string citation_status_name { get; set; }
    
        public virtual CitationStatu CitationStatu { get; set; }
        public virtual Language Language { get; set; }
    }
}
