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
    
    public partial class PlanParticipant
    {
        public Nullable<int> quantity { get; set; }
        public int participant_role_id { get; set; }
        public Nullable<int> office_id { get; set; }
        public int plan_participant_id { get; set; }
    
        public virtual Office Office { get; set; }
        public virtual ParticipantRole ParticipantRole { get; set; }
    }
}
