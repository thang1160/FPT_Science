﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ScienceAndInternationalAffairsEntities : DbContext
    {
        public ScienceAndInternationalAffairsEntities()
            : base("name=ScienceAndInternationalAffairsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CommentBase> CommentBases { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountRight> AccountRights { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<InternalUnit> InternalUnits { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Right> Rights { get; set; }
        public virtual DbSet<RightByRole> RightByRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<AcademicCollaboration> AcademicCollaborations { get; set; }
        public virtual DbSet<AcademicCollaborationStatu> AcademicCollaborationStatus { get; set; }
        public virtual DbSet<AcademicCollaborationType> AcademicCollaborationTypes { get; set; }
        public virtual DbSet<AcademicCollaborationTypeLanguage> AcademicCollaborationTypeLanguages { get; set; }
        public virtual DbSet<AcademicProgram> AcademicPrograms { get; set; }
        public virtual DbSet<CollaborationStatusHistory> CollaborationStatusHistories { get; set; }
        public virtual DbSet<CollaborationTypeDirection> CollaborationTypeDirections { get; set; }
        public virtual DbSet<CollaborationTypeDirectionLanguage> CollaborationTypeDirectionLanguages { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Procedure> Procedures { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<ArticleStatu> ArticleStatus { get; set; }
        public virtual DbSet<ArticleVersion> ArticleVersions { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<CollaborationScope> CollaborationScopes { get; set; }
        public virtual DbSet<CollaborationStatu> CollaborationStatus { get; set; }
        public virtual DbSet<MOA> MOAs { get; set; }
        public virtual DbSet<MOABonu> MOABonus { get; set; }
        public virtual DbSet<MOAPartner> MOAPartners { get; set; }
        public virtual DbSet<MOAPartnerScope> MOAPartnerScopes { get; set; }
        public virtual DbSet<MOAStatusHistory> MOAStatusHistories { get; set; }
        public virtual DbSet<MOU> MOUs { get; set; }
        public virtual DbSet<MOUBonu> MOUBonus { get; set; }
        public virtual DbSet<MOUPartner> MOUPartners { get; set; }
        public virtual DbSet<MOUPartnerScope> MOUPartnerScopes { get; set; }
        public virtual DbSet<MOUPartnerSpecialization> MOUPartnerSpecializations { get; set; }
        public virtual DbSet<MOUStatusHistory> MOUStatusHistories { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<PartnerScope> PartnerScopes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ScholarshipStatu> ScholarshipStatus { get; set; }
        public virtual DbSet<ProjectProduct> ProjectProducts { get; set; }
        public virtual DbSet<ResearchCollaboration> ResearchCollaborations { get; set; }
        public virtual DbSet<ResearchContactPoint> ResearchContactPoints { get; set; }
        public virtual DbSet<ResearchPartner> ResearchPartners { get; set; }
        public virtual DbSet<Scholarship> Scholarships { get; set; }
        public virtual DbSet<AcademicDegreeLanguage> AcademicDegreeLanguages { get; set; }
        public virtual DbSet<AcademicDegreeTypeLanguage> AcademicDegreeTypeLanguages { get; set; }
        public virtual DbSet<ConferenceCriteriaLanguage> ConferenceCriteriaLanguages { get; set; }
        public virtual DbSet<FormalityLanguage> FormalityLanguages { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<NotificationLanguage> NotificationLanguages { get; set; }
        public virtual DbSet<PaperStatusLanguage> PaperStatusLanguages { get; set; }
        public virtual DbSet<PositionLanguage> PositionLanguages { get; set; }
        public virtual DbSet<ResearchAreaLanguage> ResearchAreaLanguages { get; set; }
        public virtual DbSet<SpecializationLanguage> SpecializationLanguages { get; set; }
        public virtual DbSet<TitleLanguage> TitleLanguages { get; set; }
        public virtual DbSet<Citation> Citations { get; set; }
        public virtual DbSet<RequestCitation> RequestCitations { get; set; }
        public virtual DbSet<ApprovalProcess> ApprovalProcesses { get; set; }
        public virtual DbSet<Conference> Conferences { get; set; }
        public virtual DbSet<ConferenceParticipant> ConferenceParticipants { get; set; }
        public virtual DbSet<ConferenceStatu> ConferenceStatus { get; set; }
        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<Criterion> Criteria { get; set; }
        public virtual DbSet<EligibilityCriteria> EligibilityCriterias { get; set; }
        public virtual DbSet<Formality> Formalities { get; set; }
        public virtual DbSet<RequestConference> RequestConferences { get; set; }
        public virtual DbSet<RequestConferencePolicy> RequestConferencePolicies { get; set; }
        public virtual DbSet<ContractType> ContractTypes { get; set; }
        public virtual DbSet<PaperType> PaperTypes { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<ResearchArea> ResearchAreas { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotifyType> NotifyTypes { get; set; }
        public virtual DbSet<BaseRequest> BaseRequests { get; set; }
        public virtual DbSet<Decision> Decisions { get; set; }
        public virtual DbSet<RequestDecision> RequestDecisions { get; set; }
        public virtual DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public virtual DbSet<AcademicDegreeType> AcademicDegreeTypes { get; set; }
        public virtual DbSet<Award> Awards { get; set; }
        public virtual DbSet<CustomProfile> CustomProfiles { get; set; }
        public virtual DbSet<ProfileAcademicDegree> ProfileAcademicDegrees { get; set; }
        public virtual DbSet<WorkingProcess> WorkingProcesses { get; set; }
        public virtual DbSet<AuthorInvention> AuthorInventions { get; set; }
        public virtual DbSet<AuthorPaper> AuthorPapers { get; set; }
        public virtual DbSet<Invention> Inventions { get; set; }
        public virtual DbSet<InventionType> InventionTypes { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<PaperCriteria> PaperCriterias { get; set; }
        public virtual DbSet<PaperStatu> PaperStatus { get; set; }
        public virtual DbSet<PaperWithCriteria> PaperWithCriterias { get; set; }
        public virtual DbSet<RequestInvention> RequestInventions { get; set; }
        public virtual DbSet<RequestPaper> RequestPapers { get; set; }
        public virtual DbSet<Scimagojr> Scimagojrs { get; set; }
        public virtual DbSet<AcademicActivity> AcademicActivities { get; set; }
        public virtual DbSet<AcademicActivityLanguage> AcademicActivityLanguages { get; set; }
        public virtual DbSet<AcademicActivityPhase> AcademicActivityPhases { get; set; }
        public virtual DbSet<AcademicActivityPhaseLanguage> AcademicActivityPhaseLanguages { get; set; }
        public virtual DbSet<AcademicActivityType> AcademicActivityTypes { get; set; }
        public virtual DbSet<AcademicActivityTypeLanguage> AcademicActivityTypeLanguages { get; set; }
        public virtual DbSet<ActivityExpenseCategory> ActivityExpenseCategories { get; set; }
        public virtual DbSet<ActivityExpenseDetail> ActivityExpenseDetails { get; set; }
        public virtual DbSet<ActivityExpenseType> ActivityExpenseTypes { get; set; }
        public virtual DbSet<ActivityInfo> ActivityInfoes { get; set; }
        public virtual DbSet<ActivityPartner> ActivityPartners { get; set; }
        public virtual DbSet<AnswerType> AnswerTypes { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<ParticipantRole> ParticipantRoles { get; set; }
        public virtual DbSet<PlanParticipant> PlanParticipants { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<ConferenceStatusLanguage> ConferenceStatusLanguages { get; set; }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
