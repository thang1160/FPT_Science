﻿using ENTITIES;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InternationalCollaboration.Collaboration.MemorandumOfUnderstanding
{
    class BasicInfoMOURepo
    {
        readonly ScienceAndInternationalAffairsEntities db = new ScienceAndInternationalAffairsEntities();
        public MOUBasicInfo getBasicInfoMOU(int mou_id)
        {
            try
            {
                string sql_mouBasicInfo =
                    @"select 
                        t1.mou_id,t1.mou_code,t2.office_abbreviation,t1.mou_end_date,
                        t5.mou_status_name,t4.reason,t1.evidence,t1.mou_note
                        from IA_Collaboration.MOU t1
                        inner join General.Office t2 on t1.office_id = t2.office_id
                        inner join
                        (select max([datetime]) as 'maxdate',mou_status_id, mou_id
                        from IA_Collaboration.MOUStatusHistory 
                        group by mou_status_id, mou_id) t3 on t3.mou_id = t1.mou_id
                        inner join IA_Collaboration.MOUStatusHistory t4 on 
                        t4.datetime = t3.maxdate and t4.mou_id = t4.mou_id and t4.mou_status_id = t3.mou_status_id
                        inner join iA_Collaboration.MOUStatus t5 on
                        t5.mou_status_id = t3.mou_status_id
                        where t1.mou_id = @mou_id ";
                string sql_mouStartDateAndScopes =
                    @"select t2.*,t3.mou_start_date from
                        (select mou_id, STRING_AGG(scope_abbreviation,', ') as scopes from
                        (select distinct mou_id,scope_abbreviation from IA_Collaboration.MOUPartnerScope tb1a
                        left join IA_MasterData.CollaborationScope tb1b on
                        tb1a.scope_id = tb1b.scope_id
                        where mou_id = @mou_id) t1
                        group by mou_id) t2
                        left join 
                        (select max(mou_start_date) as mou_start_date,mou_id
                        from IA_Collaboration.MOUPartner
                        where mou_id = @mou_id
                        group by mou_id) t3
                        on t3.mou_id = t2.mou_id ";
                MOUBasicInfo basicInfo = db.Database.SqlQuery<MOUBasicInfo>(sql_mouBasicInfo,
                    new SqlParameter("mou_id", mou_id)).First();
                MOUBasicInfo dateAndScopes = db.Database.SqlQuery<MOUBasicInfo>(sql_mouStartDateAndScopes,
                    new SqlParameter("mou_id", mou_id)).First();
                handlingMOUData(basicInfo, dateAndScopes);
                return basicInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void handlingMOUData(MOUBasicInfo basicInfo, MOUBasicInfo dateAndScopes)
        {
            //handle date display
            basicInfo.mou_end_date_string = basicInfo.mou_end_date.ToString("dd'/'MM'/'yyyy");
            basicInfo.mou_start_date_string = dateAndScopes.mou_start_date.ToString("dd'/'MM'/'yyyy");
            basicInfo.scopes = dateAndScopes.scopes;
        }
        public List<ExtraMOU> listAllExtraMOU()
        {
            try
            {
                string sql_mouExList =
                    @"select t1.mou_bonus_code, t1.mou_bonus_decision_date,t1.mou_bonus_end_date,
                        t3.partner_name,t4.scope_abbreviation,t1.evidence,t1.mou_id
                        from IA_Collaboration.MOUBonus t1 left join 
                        IA_Collaboration.MOUPartnerScope t2 on 
                        t1.mou_bonus_id = t2.mou_bonus_id inner join 
                        IA_Collaboration.Partner t3 on t3.partner_id = t2.partner_id
                        inner join IA_MasterData.CollaborationScope t4 on t4.scope_id = t2.scope_id";
                List<ExtraMOU> mouExList = db.Database.SqlQuery<ExtraMOU>(sql_mouExList).ToList();
                handlingExMOUListData(mouExList);
                return mouExList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void handlingExMOUListData(List<ExtraMOU> mouExList)
        {
            ExtraMOU previousItem = null;
            foreach (ExtraMOU item in mouExList.ToList())
            {
                if (previousItem == null) //first record
                {
                    previousItem = item;
                    previousItem.mou_bonus_decision_date_string = item.mou_bonus_decision_date.ToString("dd'/'MM'/'yyyy");
                    previousItem.mou_bonus_end_date_string = item.mou_bonus_end_date.ToString("dd'/'MM'/'yyyy");
                }
                else
                {
                    if (item.mou_id.Equals(previousItem.mou_id))
                    {
                        if (!previousItem.scope_abbreviation.Contains(item.scope_abbreviation))
                        {
                            previousItem.scope_abbreviation = previousItem.scope_abbreviation + "," + item.scope_abbreviation;
                        }
                        //then remove current object
                        mouExList.Remove(item);
                    }
                    else
                    {
                        previousItem = item;
                    }
                }
            }
            return;
        }
        public void editMOUBasicInfo(int mou_id, MOUBasicInfo newBasicInfo)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    string sql_maxDate = @"select t2.* from
                        (
                        select max([datetime]) as 'maxdate',mou_status_id, mou_id
                        from IA_Collaboration.MOUStatusHistory 
                        where mou_id = @mou_id
                        group by mou_status_id, mou_id) t1 
                        inner join IA_Collaboration.MOUStatusHistory t2
                        on t2.mou_id = t1.mou_id and t2.mou_status_id = t1.mou_status_id";
                    //update basicInfo
                    MOU mou = db.MOUs.Find(mou_id);
                    mou.mou_code = newBasicInfo.mou_code;
                    mou.mou_end_date = newBasicInfo.mou_end_date;
                    mou.mou_note = newBasicInfo.mou_note;
                    mou.evidence = newBasicInfo.evidence;
                    mou.office_id = newBasicInfo.office_id;
                    db.Entry(mou).State = EntityState.Modified;

                    MOUStatusHistory oldInfo = db.Database.SqlQuery<MOUStatusHistory>(sql_maxDate,
                        new SqlParameter("mou_id", mou_id)).First();
                    if (oldInfo.reason is null)
                    {
                        oldInfo.reason = "";
                    }
                    if (!(oldInfo.reason.Equals(newBasicInfo.reason) && oldInfo.mou_status_id == newBasicInfo.mou_status_id))
                    {
                        MOUStatusHistory m = new MOUStatusHistory();
                        m.mou_status_id = newBasicInfo.mou_status_id;
                        m.reason = newBasicInfo.reason;
                        m.mou_id = newBasicInfo.mou_id;
                        m.datetime = DateTime.Now;
                        db.MOUStatusHistories.Add(m);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return;
        }
        public ExtraMOU getExtraMOUDetail(int mou_id)
        {
            try
            {
                string sql_mouEx =
                    @"select t1.mou_bonus_code, t1.mou_bonus_decision_date,t1.mou_bonus_end_date,
                        t3.partner_name,t4.scope_abbreviation,t1.evidence,t1.mou_id
                        from IA_Collaboration.MOUBonus t1 left join 
                        IA_Collaboration.MOUPartnerScope t2 on 
                        t1.mou_bonus_id = t2.mou_bonus_id inner join 
                        IA_Collaboration.Partner t3 on t3.partner_id = t2.partner_id
                        inner join IA_MasterData.CollaborationScope t4 on t4.scope_id = t2.scope_id
                        where t1.mou_id = @mou_id ";

                List<ExtraMOU> mouExList = db.Database.SqlQuery<ExtraMOU>(sql_mouEx
                    , new SqlParameter("mou_id", mou_id)).ToList();
                ExtraMOU mouEx = handlingExMOUDetailData(mouExList);
                return mouEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ExtraMOU handlingExMOUDetailData(List<ExtraMOU> mouExList)
        {
            ExtraMOU baseItem = null;
            //Partner
            foreach (ExtraMOU item in mouExList.ToList())
            {
                if (baseItem == null) //first record
                {
                    baseItem = item;
                    baseItem.mou_bonus_decision_date_string = item.mou_bonus_decision_date.ToString("dd'/'MM'/'yyyy");
                    baseItem.mou_bonus_end_date_string = item.mou_bonus_end_date.ToString("dd'/'MM'/'yyyy");
                    baseItem.ListPartnerExMOU.Add(new CustomPartner(item.partner_id, item.partner_name));
                }
                else
                {
                    if (item.partner_id == baseItem.partner_id)
                    {
                        baseItem.ListPartnerExMOU.Add(new CustomPartner(item.partner_id, item.partner_name));
                    }
                }
            }

            //Scope
            foreach (ExtraMOU item in mouExList.ToList())
            {
                foreach (CustomPartner partnerItem in baseItem.ListPartnerExMOU)
                {
                    if (partnerItem.partner_id == item.partner_id)
                    {
                        partnerItem.ListScopeExMOU.Add(new CustomScope(item.scope_id, item.scope_abbreviation));
                        break;
                    }
                }
            }
            return baseItem;
        }
        //public void addExtraMOU(ExtraMOU input)
        //{
        //    using (DbContextTransaction transaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            //add MOUBonus
        //            MOUBonu mb = new MOUBonu();
        //            mb.mou_bonus_code = input.mou_bonus_code;
        //            mb.mou_bonus_decision_date = input.mou_bonus_decision_date;
        //            mb.mou_bonus_end_date = input.mou_bonus_end_date;
        //            mb.mou_id = input.mou_id;
        //            mb.evidence = input.evidence;
        //            db.MOUBonus.Add(mb);
        //            MOUBonu addObj = db.MOUBonus.Where(x => x.mou_bonus_code.Equals(mb.mou_bonus_code)).First();

        //            //add MOuPartnerScope
        //            foreach (CustomPartner cp in input.ListPartnerExMOU.ToList())
        //            {
        //                foreach (CustomScope cs in cp.ListScopeExMOU.ToList())
        //                {
        //                    MOUPartnerScope m = new MOUPartnerScope();
        //                    m.mou_id = input.mou_id;
        //                    m.partner_id = cp.partner_id;
        //                    m.scope_id = cs.scope_id;
        //                    m.mou_bonus_id = addObj.mou_bonus_id;
        //                    db.MOUPartnerScopes.Add(m);
        //                }
        //            }

        //            db.SaveChanges();
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}
        //public void editExtraMOU(ExtraMOU input, int mou_bonus_id)
        //{
        //    using (DbContextTransaction transaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            //edit MOUBonus
        //            MOUBonu mb = db.MOUBonus.Find(mou_bonus_id);
        //            mb.mou_bonus_code = input.mou_bonus_code;
        //            mb.mou_bonus_decision_date = input.mou_bonus_decision_date;
        //            mb.mou_bonus_end_date = input.mou_bonus_end_date;
        //            mb.mou_id = input.mou_id;
        //            mb.evidence = input.evidence;
        //            db.Entry(mb).State = EntityState.Modified;

        //            //finding old exScope of exMOU.
        //            List<MOUPartnerScope> exList = db.MOUPartnerScopes.Where(x => x.mou_bonus_id == mou_bonus_id).ToList();
        //            exList.Clear();
        //            db.Entry(exList).State = EntityState.Modified;

        //            //add new record of MOuPartnerScope
        //            foreach (CustomPartner cp in input.ListPartnerExMOU.ToList())
        //            {
        //                foreach (CustomScope cs in cp.ListScopeExMOU.ToList())
        //                {
        //                    MOUPartnerScope m = new MOUPartnerScope();
        //                    m.mou_id = input.mou_id;
        //                    m.partner_id = cp.partner_id;
        //                    m.scope_id = cs.scope_id;
        //                    m.mou_bonus_id = mou_bonus_id;
        //                    db.MOUPartnerScopes.Add(m);
        //                }
        //            }

        //            db.SaveChanges();
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}
        public void deleteExtraMOU(int mou_bonus_id)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //finding old exScope of exMOU.
                    List<MOUPartnerScope> exList = db.MOUPartnerScopes.Where(x => x.mou_bonus_id == mou_bonus_id).ToList();
                    exList.Clear();
                    db.Entry(exList).State = EntityState.Modified;

                    //add new record of MOuPartnerScope
                    MOUBonu m = db.MOUBonus.Find(mou_bonus_id);
                    db.MOUBonus.Remove(m);

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public string getNewExtraMOUCode(int mou_id)
        {
            try
            {
                string sql_countInYear = @"select count(*) from IA_Collaboration.MOU where mou_code like @year";
                string sql_checkDup = @"select count(*) from IA_Collaboration.MOUBonus where mou_bonus_code = @newCode";
                bool isDuplicated = false;
                string newCode = "";
                int countInYear = db.Database.SqlQuery<int>(sql_countInYear,
                        new SqlParameter("year", '%' + DateTime.Now.Year + '%')).First();
                int countInMOU = db.MOUBonus.Where(x => x.mou_id == mou_id).Count();

                //fix duplicate mou_code:
                countInYear++;
                do
                {
                    countInMOU++;
                    newCode = DateTime.Now.Year + "/" + countInYear + "_BS/" + countInMOU;
                    isDuplicated = db.Database.SqlQuery<int>(sql_checkDup,
                        new SqlParameter("newCode", newCode)).First() == 1 ? true : false;
                } while (isDuplicated);
                return newCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Office> GetOffice()
        {
            try
            {
                string sql_unitList = @"select * from General.Office";
                List<Office> unitList = db.Database.SqlQuery<Office>(sql_unitList).ToList();
                return unitList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Specialization> GetSpecializations()
        {
            try
            {
                string sql_speList = @"select * from General.Specialization";
                List<Specialization> speList = db.Database.SqlQuery<Specialization>(sql_speList).ToList();
                return speList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CollaborationScope> GetCollaborationScopes()
        {
            try
            {
                string sql_scopeList = @"select * from IA_MasterData.CollaborationScope";
                List<CollaborationScope> scopeList = db.Database.SqlQuery<CollaborationScope>(sql_scopeList).ToList();
                return scopeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public class MOUBasicInfo
        {
            public MOUBasicInfo() { }
            public int mou_id { get; set; }
            public string mou_code { get; set; }
            public string evidence { get; set; }
            public string scopes { get; set; }
            public string reason { get; set; }
            public DateTime mou_end_date { get; set; }
            public DateTime mou_start_date { get; set; }
            public string mou_end_date_string { get; set; }
            public string mou_start_date_string { get; set; }
            public string mou_note { get; set; }
            public string office_abbreviation { get; set; }
            public string mou_status_name { get; set; }
            public int office_id { get; set; }
            public int mou_status_id { get; set; }
        }
        public class ExtraMOU
        {
            public string mou_bonus_code { get; set; }
            public string mou_bonus_decision_date_string { get; set; }
            public string mou_bonus_end_date_string { get; set; }
            public DateTime mou_bonus_end_date { get; set; }
            public DateTime mou_bonus_decision_date { get; set; }
            public string partner_name { get; set; }
            public string scope_abbreviation { get; set; }
            public string evidence { get; set; }
            public int mou_id { get; set; }
            public int partner_id { get; set; }
            public int scope_id { get; set; }
            public List<CustomPartner> ListPartnerExMOU { get; set; }
        }
        public class CustomPartner
        {
            public CustomPartner(int partner_id, string partner_name)
            {
                this.partner_id = partner_id;
                this.partner_name = partner_name;
            }
            public CustomPartner() { }
            public List<CustomScope> ListScopeExMOU { get; set; }
            public int partner_id { get; set; }
            public string partner_name { get; set; }
        }
        public class CustomScope
        {
            public CustomScope(int scope_id, string scope_name)
            {
                this.scope_id = scope_id;
                this.scope_name = scope_name;
            }
            public CustomScope() { }
            public int scope_id { get; set; }
            public string scope_name { get; set; }
        }
    }
}
