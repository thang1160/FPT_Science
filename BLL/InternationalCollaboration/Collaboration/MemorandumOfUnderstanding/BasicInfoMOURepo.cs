﻿using ENTITIES;
using ENTITIES.CustomModels;
using ENTITIES.CustomModels.InternationalCollaboration.Collaboration.MemorandumOfUnderstanding.MOUBasicInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.InternationalCollaboration.Collaboration.MemorandumOfUnderstanding
{
    public class BasicInfoMOURepo
    {
        readonly ScienceAndInternationalAffairsEntities db = new ScienceAndInternationalAffairsEntities();
        public MOUBasicInfo GetBasicInfoMOU(int mou_id)
        {
            try
            {
                string sql_mouBasicInfo =
                    @"select 
                        t1.mou_id,t1.mou_code,t2.office_abbreviation,t1.mou_end_date,
                        t5.mou_status_name,t4.reason,t1.evidence,t6.name as file_name,t6.link as file_link,
						t6.file_drive_id ,t1.mou_note,
                        t1.office_id,t5.mou_status_id
                        from IA_Collaboration.MOU t1
                        inner join General.Office t2 on t1.office_id = t2.office_id
                        inner join
                        (select a.mou_id,a.mou_status_id,max_date from IA_Collaboration.MOUStatusHistory a
                        inner join 
                        (select max(datetime) as max_date,mou_id from IA_Collaboration.MOUStatusHistory
                        group by mou_id) b on
                        a.datetime = b.max_date and a.mou_id = b.mou_id) t3 on t3.mou_id = t1.mou_id
                        inner join IA_Collaboration.MOUStatusHistory t4 on 
                        t4.datetime = t3.max_date and t4.mou_id = t4.mou_id and t4.mou_status_id = t3.mou_status_id
                        inner join IA_Collaboration.CollaborationStatus t5 on
                        t5.mou_status_id = t3.mou_status_id
						left join General.[File] t6 on
						t6.file_id = t1.evidence
                        where t1.mou_id = @mou_id ";
                string sql_mouStartDateAndScopes =
                    @"select t2.*,t3.mou_start_date from
                        (select mou_id, STRING_AGG(scope_abbreviation,', ') as scopes from
                        (select distinct mou_id,scope_abbreviation from 
                        (select mou_id,partner_id, scope_id
                        from IA_Collaboration.MOUPartnerScope t1a left join 
                        IA_Collaboration.PartnerScope t2a 
                        on t2a.partner_scope_id = t1a.partner_scope_id
                        where mou_bonus_id is null ) tb1a
                        left join IA_Collaboration.CollaborationScope tb1b on
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
                HandlingMOUData(basicInfo, dateAndScopes);
                return basicInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HandlingMOUData(MOUBasicInfo basicInfo, MOUBasicInfo dateAndScopes)
        {
            //handle date display
            basicInfo.mou_end_date_string = basicInfo.mou_end_date.ToString("dd'/'MM'/'yyyy");
            basicInfo.mou_start_date_string = dateAndScopes.mou_start_date.ToString("dd'/'MM'/'yyyy");
            basicInfo.scopes = dateAndScopes.scopes;
        }
        public List<ExtraMOU> ListAllExtraMOU(int mou_id)
        {
            try
            {
                string sql_mouExList =
                    @"select t1.mou_bonus_code, t1.mou_bonus_decision_date, isnull(t1.mou_bonus_end_date,'') as mou_bonus_end_date,
                        t4.partner_name,t5.scope_abbreviation,t6.link as evidence,t1.mou_id,t1.mou_bonus_id
                        from IA_Collaboration.MOUBonus t1 left join 
                        IA_Collaboration.MOUPartnerScope t2 on 
                        t1.mou_bonus_id = t2.mou_bonus_id left join 
                        IA_Collaboration.PartnerScope t3 on
                        t3.partner_scope_id = t2.partner_scope_id
                        left join 
                        IA_Collaboration.Partner t4 on t4.partner_id = t3.partner_id
                        left join IA_Collaboration.CollaborationScope t5 on t5.scope_id = t3.scope_id
						left join General.[File] t6 on
						t6.file_id = t1.evidence
                        where t1.mou_id = @mou_id
                        order by mou_bonus_id, partner_name";
                List<ExtraMOU> mouExList = db.Database.SqlQuery<ExtraMOU>(sql_mouExList,
                    new SqlParameter("mou_id", mou_id)).ToList();
                HandlingExMOUListData(mouExList);
                return mouExList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HandlingExMOUListData(List<ExtraMOU> mouExList)
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
                    if (item.mou_bonus_id.Equals(previousItem.mou_bonus_id) && item.partner_name.Equals(previousItem.partner_name))
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
                        previousItem.mou_bonus_decision_date_string = item.mou_bonus_decision_date.ToString("dd'/'MM'/'yyyy");
                        previousItem.mou_bonus_end_date_string = item.mou_bonus_end_date.ToString("dd'/'MM'/'yyyy");
                    }
                }
            }
            return;
        }
        public void EditMOUBasicInfo(int mou_id, MOUBasicInfo newBasicInfo, HttpPostedFileBase evidence,
            int old_file_number, int new_file_number)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //handling with files.
                    Google.Apis.Drive.v3.Data.File f = new Google.Apis.Drive.v3.Data.File();
                    File evidence_file = new File();
                    if (old_file_number == 0)
                    {
                        if (new_file_number == 1)
                        {
                            //Add new file
                            f = new MOURepo().UploadEvidenceFile(evidence, newBasicInfo.mou_code, 1, false);
                            evidence_file = new MOURepo().SaveFile(f, evidence);
                        }
                    }
                    else if (old_file_number == 1)
                    {
                        int file_id = (int)db.MOUs.Find(mou_id).evidence;
                        string old_file_drive_id = db.Files.Find(file_id).file_drive_id;

                        if (new_file_number == 0)
                        {
                            //Delete old file
                            MOURepo.DeleteEvidenceFile(old_file_drive_id);
                            db.Files.Remove(db.Files.Find(file_id));
                            db.SaveChanges();
                        }
                        else if (new_file_number == 1)
                        {
                            if (evidence != null)
                            {
                                //Update new file
                                f = GoogleDriveService.UpdateFile(evidence.FileName, evidence.InputStream, evidence.ContentType, old_file_drive_id);
                                db.Files.Remove(db.Files.Find(file_id));
                                evidence_file = new MOURepo().SaveFile(f, evidence);
                                db.SaveChanges();
                            }
                            else
                            {
                                evidence_file.file_id = file_id;
                            }
                        }
                    }

                    DateTime mou_end_date = DateTime.ParseExact(newBasicInfo.mou_end_date_string, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //update basicInfo
                    MOU mou = db.MOUs.Find(mou_id);
                    mou.mou_code = newBasicInfo.mou_code;
                    mou.mou_end_date = mou_end_date;
                    mou.mou_note = newBasicInfo.mou_note;
                    mou.office_id = newBasicInfo.office_id;
                    if (evidence_file.file_id == 0)
                    {
                        mou.evidence = null;
                    }
                    else
                    {
                        mou.evidence = evidence_file.file_id;
                    }
                    db.Entry(mou).State = EntityState.Modified;
                    db.SaveChanges();

                    //update MOUStatusHistory
                    MOUStatusHistory m = new MOUStatusHistory
                    {
                        mou_status_id = newBasicInfo.mou_status_id,
                        reason = newBasicInfo.reason,
                        mou_id = mou_id,
                        datetime = DateTime.Now
                    };
                    db.MOUStatusHistories.Add(m);
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
        public ExMOUAdd GetExtraMOUDetail(int mou_bonus_id, int mou_id)
        {
            try
            {
                string sql_mouEx =
                    @"select t1.mou_bonus_code, t1.mou_bonus_decision_date,isnull(t1.mou_bonus_end_date,'') as mou_bonus_end_date,
                        t4.partner_name,t5.scope_abbreviation,t6.file_drive_id,t6.name as file_name,t1.mou_id,t1.mou_bonus_id,
                        ISNULL(t5.scope_id, 0) as scope_id, ISNULL(t4.partner_id, 0) as partner_id
                        from IA_Collaboration.MOUBonus t1 left join 
                        IA_Collaboration.MOUPartnerScope t2 on 
                        t1.mou_bonus_id = t2.mou_bonus_id left join 
                        IA_Collaboration.PartnerScope t3 on
                        t3.partner_scope_id = t2.partner_scope_id
                        left join 
                        IA_Collaboration.Partner t4 on t4.partner_id = t3.partner_id
                        left join IA_Collaboration.CollaborationScope t5 on t5.scope_id = t3.scope_id
						left join General.[File] t6 on
						t6.file_id = t1.evidence
                        where t1.mou_id = @mou_id and t1.mou_bonus_id = @mou_bonus_id order by partner_id";
                List<ExtraMOU> mouExList = db.Database.SqlQuery<ExtraMOU>(sql_mouEx
                    , new SqlParameter("mou_id", mou_id)
                    , new SqlParameter("mou_bonus_id", mou_bonus_id)).ToList();
                ExMOUAdd mouEx = HandlingExMOUDetailData(mouExList);
                return mouEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ExMOUAdd HandlingExMOUDetailData(List<ExtraMOU> mouExList)
        {
            ExMOUAdd newObj = new ExMOUAdd
            {
                file_drive_id = mouExList[0].file_drive_id,
                file_name = mouExList[0].file_name,
                ExBasicInfo = new ExBasicInfo(),
                PartnerScopeInfo = new List<PartnerScopeInfo>()
            };
            //Partner
            foreach (ExtraMOU item in mouExList.ToList())
            {
                newObj.ExBasicInfo.ex_mou_code = item.mou_bonus_code;
                newObj.ExBasicInfo.ex_mou_end_date = item.mou_bonus_end_date.ToString("dd'/'MM'/'yyyy");
                newObj.ExBasicInfo.ex_mou_sign_date = item.mou_bonus_decision_date.ToString("dd'/'MM'/'yyyy");
                PartnerScopeInfo p = newObj.PartnerScopeInfo.Find(x => x.partner_id == item.partner_id);
                if (p == null)
                {
                    PartnerScopeInfo obj = new PartnerScopeInfo
                    {
                        scopes_id = new List<int>(),
                        partner_id = item.partner_id
                    };
                    obj.scopes_id.Add(item.scope_id);
                    newObj.PartnerScopeInfo.Add(obj);
                }
                else
                {
                    p.scopes_id.Add(item.scope_id);
                }
                //Case: remove PartnerScopeInfo if null
                if (newObj.PartnerScopeInfo.Count == 1)
                {
                    if (newObj.PartnerScopeInfo[0].partner_id == 0)
                    {
                        newObj.PartnerScopeInfo.RemoveAt(0);
                    }
                }
            }
            return newObj;
        }
        public void AddExtraMOU(ExMOUAdd input, int mou_id, BLL.Authen.LoginRepo.User user, HttpPostedFileBase evidence)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //File handling.
                    Google.Apis.Drive.v3.Data.File f = new Google.Apis.Drive.v3.Data.File();
                    if (evidence != null)
                    {
                        f = new MOURepo().UploadEvidenceFile(evidence, db.MOUs.Find(mou_id).mou_code, 2, false);
                    }
                    File evidence_file = new MOURepo().SaveFile(f, evidence);
                    int? evidence_value;
                    if (evidence_file.file_id == 0)
                    {
                        evidence_value = null;
                    }
                    else
                    {
                        evidence_value = evidence_file.file_id;
                    }

                    List<PartnerScope> totalRelatedPS = new List<PartnerScope>();
                    DateTime sign_date = DateTime.ParseExact(input.ExBasicInfo.ex_mou_sign_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime? end_date = null;
                    if (input.ExBasicInfo.ex_mou_end_date != "")
                    {
                        end_date = DateTime.ParseExact(input.ExBasicInfo.ex_mou_end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    //add MOUBonus

                    MOUBonu mb = db.MOUBonus.Add(new MOUBonu
                    {
                        mou_bonus_code = input.ExBasicInfo.ex_mou_code,
                        mou_bonus_decision_date = sign_date,
                        mou_bonus_end_date = end_date,
                        mou_id = mou_id,
                        account_id = user is null ? 1 : user.account.account_id,
                        add_time = DateTime.Now,
                        evidence = evidence_value
                    });
                    //checkpoint 1
                    db.SaveChanges();

                    if (input.PartnerScopeInfo != null)
                    {
                        foreach (PartnerScopeInfo partnerScopeItem in input.PartnerScopeInfo.ToList())
                        {
                            foreach (int scopeItem in partnerScopeItem.scopes_id.ToList())
                            {
                                //PartnerScope
                                //MOUPartnerScope
                                PartnerScope ps = db.PartnerScopes.Where(x => x.partner_id == partnerScopeItem.partner_id && x.scope_id == scopeItem).FirstOrDefault();
                                if (ps is null)
                                {
                                    PartnerScope psAdded = db.PartnerScopes.Add(new PartnerScope
                                    {
                                        partner_id = partnerScopeItem.partner_id,
                                        scope_id = scopeItem,
                                        reference_count = 1
                                    });
                                    db.MOUPartnerScopes.Add(new MOUPartnerScope
                                    {
                                        mou_id = mou_id,
                                        mou_bonus_id = mb.mou_bonus_id,
                                        partner_scope_id = psAdded.partner_scope_id
                                    });
                                    totalRelatedPS.Add(psAdded);
                                }
                                else
                                {
                                    ps.reference_count += 1;
                                    db.Entry(ps).State = EntityState.Modified;
                                    db.MOUPartnerScopes.Add(new MOUPartnerScope
                                    {
                                        mou_id = mou_id,
                                        mou_bonus_id = mb.mou_bonus_id,
                                        partner_scope_id = ps.partner_scope_id
                                    });
                                    totalRelatedPS.Add(ps);
                                }
                            }
                        }
                        //checkpoint 2
                        db.SaveChanges();
                    }
                    transaction.Commit();

                    //change status corressponding MOU/MOA
                    using (DbContextTransaction dbContext = db.Database.BeginTransaction())
                    {
                        try
                        {
                            List<int> listPS = totalRelatedPS.Select(x => x.partner_scope_id).Distinct().ToList();
                            new AutoActiveInactive().changeStatusMOUMOA(listPS, db);
                            dbContext.Commit();
                        }
                        catch (Exception e)
                        {
                            dbContext.Rollback();
                            throw e;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public void EditExtraMOU(ExMOUAdd input, BLL.Authen.LoginRepo.User user, int old_file_number, int new_file_number,
            HttpPostedFileBase evidence, int mou_id)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //File handling.
                    Google.Apis.Drive.v3.Data.File f = new Google.Apis.Drive.v3.Data.File();
                    File evidence_file = new File();
                    if (old_file_number == 0)
                    {
                        if (new_file_number == 1)
                        {
                            //Add new file
                            f = new MOURepo().UploadEvidenceFile(evidence, db.MOUs.Find(mou_id).mou_code, 2, false);
                            evidence_file = new MOURepo().SaveFile(f, evidence);
                        }
                    }
                    else if (old_file_number == 1)
                    {
                        //int file_id = (int)db.MOUs.Find(mou_id).evidence;
                        int file_id = (int)db.MOUBonus.Where(x => x.mou_id == mou_id).First().evidence;
                        string old_file_drive_id = db.Files.Find(file_id).file_drive_id;

                        if (new_file_number == 0)
                        {
                            //Delete old file
                            MOURepo.DeleteEvidenceFile(old_file_drive_id);
                            db.Files.Remove(db.Files.Find(file_id));
                            db.SaveChanges();
                        }
                        else if (new_file_number == 1)
                        {
                            if (evidence != null)
                            {
                                //Update new file
                                f = GoogleDriveService.UpdateFile(evidence.FileName, evidence.InputStream, evidence.ContentType, old_file_drive_id);
                                db.Files.Remove(db.Files.Find(file_id));
                                evidence_file = new MOURepo().SaveFile(f, evidence);
                                db.SaveChanges();
                            }
                            else
                            {
                                evidence_file.file_id = file_id;
                            }
                        }
                    }

                    List<PartnerScope> totalRelatedPS = new List<PartnerScope>();
                    DateTime sign_date = DateTime.ParseExact(input.ExBasicInfo.ex_mou_sign_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime? end_date = null;
                    if (input.ExBasicInfo.ex_mou_end_date != "")
                    {
                        end_date = DateTime.ParseExact(input.ExBasicInfo.ex_mou_end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    //edit MOUBonus
                    MOUBonu mb = db.MOUBonus.Find(input.mou_bonus_id);
                    if (evidence_file.file_id == 0)
                    {
                        mb.evidence = null;
                    }
                    else
                    {
                        mb.evidence = evidence_file.file_id;
                    }
                    mb.mou_bonus_code = input.ExBasicInfo.ex_mou_code;
                    mb.mou_bonus_decision_date = sign_date;
                    mb.mou_bonus_end_date = end_date;
                    mb.account_id = user is null ? 1 : user.account.account_id;
                    mb.add_time = DateTime.Now;
                    db.Entry(mb).State = EntityState.Modified;

                    //get old 
                    List<MOUPartnerScope> mouPSList = db.MOUPartnerScopes.Where(x => x.mou_bonus_id == input.mou_bonus_id).ToList();
                    foreach (MOUPartnerScope mouPSItem in mouPSList.ToList())
                    {
                        //decrese ref_count of old PartnerScope records.
                        PartnerScope oldPS = db.PartnerScopes.Find(mouPSItem.partner_scope_id);
                        oldPS.reference_count -= 1;
                        db.Entry(oldPS).State = EntityState.Modified;
                        totalRelatedPS.Add(oldPS);
                    }
                    //del records of MOUPartnerScope.
                    db.MOUPartnerScopes.RemoveRange(mouPSList);
                    db.SaveChanges();

                    //Check partnerScope existed and handle it.
                    //add data to MOUPartnerScope.
                    if (input.PartnerScopeInfo != null)
                    {
                        foreach (PartnerScopeInfo psi in input.PartnerScopeInfo.ToList())
                        {
                            foreach (int scope in psi.scopes_id.ToList())
                            {
                                PartnerScope psCheck = db.PartnerScopes.Where(x => x.partner_id == psi.partner_id && x.scope_id == scope).FirstOrDefault();
                                if (psCheck is null)
                                {
                                    PartnerScope psAdded = db.PartnerScopes.Add(new PartnerScope
                                    {
                                        partner_id = psi.partner_id,
                                        scope_id = scope,
                                        reference_count = 1
                                    });
                                    db.MOUPartnerScopes.Add(new MOUPartnerScope
                                    {
                                        partner_scope_id = psAdded.partner_scope_id,
                                        mou_id = mb.mou_id,
                                        mou_bonus_id = input.mou_bonus_id
                                    });
                                    totalRelatedPS.Add(psAdded);
                                }
                                else
                                {
                                    psCheck.reference_count += 1;
                                    db.MOUPartnerScopes.Add(new MOUPartnerScope
                                    {
                                        partner_scope_id = psCheck.partner_scope_id,
                                        mou_id = mb.mou_id,
                                        mou_bonus_id = input.mou_bonus_id
                                    });
                                    totalRelatedPS.Add(psCheck);
                                }
                            }
                        }
                    }
                    //checkpoint 2
                    db.SaveChanges();
                    transaction.Commit();

                    //change status corressponding MOU/MOA
                    using (DbContextTransaction dbContext = db.Database.BeginTransaction())
                    {
                        try
                        {
                            List<int> listPS = totalRelatedPS.Select(x => x.partner_scope_id).Distinct().ToList();
                            new AutoActiveInactive().changeStatusMOUMOA(listPS, db);
                            dbContext.Commit();
                        }
                        catch (Exception e)
                        {
                            dbContext.Rollback();
                            throw e;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public void DeleteExtraMOU(int mou_bonus_id)
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //finding old exScope of exMOU.
                    List<MOUPartnerScope> exList = db.MOUPartnerScopes.Where(x => x.mou_bonus_id == mou_bonus_id).ToList();
                    List<int> listPS = exList.Select(x => x.partner_scope_id).Distinct().ToList();
                    db.MOUPartnerScopes.RemoveRange(exList);
                    db.SaveChanges();

                    //add new record of MOuPartnerScope
                    MOUBonu m = db.MOUBonus.Find(mou_bonus_id);
                    db.MOUBonus.Remove(m);

                    db.SaveChanges();
                    transaction.Commit();

                    //change status corressponding MOU/MOA
                    using (DbContextTransaction dbContext = db.Database.BeginTransaction())
                    {
                        try
                        {
                            new AutoActiveInactive().changeStatusMOUMOA(listPS, db);
                            dbContext.Commit();
                        }
                        catch (Exception e)
                        {
                            dbContext.Rollback();
                            throw e;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public string GetNewExtraMOUCode(int mou_id)
        {
            try
            {
                bool isDuplicated = false;
                string newCode = "";

                string old_mou_code = db.MOUs.Find(mou_id).mou_code;
                int count_ex_mou = db.MOUBonus.Where(x => x.mou_id == mou_id).Count();

                //fix duplicate mou_code:
                do
                {
                    count_ex_mou++;
                    newCode = old_mou_code + "_BS/" + count_ex_mou;
                    isDuplicated = !(db.MOUBonus.Where(x => x.mou_bonus_code.Equals(newCode)).FirstOrDefault() is null);
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
        public List<Partner> GetPartnerExMOU(int mou_id)
        {
            try
            {
                string sql_partnerList = @"select t2.*  
                    from IA_Collaboration.MOUPartner t1 left join 
                    IA_Collaboration.Partner t2 on t2.partner_id = t1.partner_id
                    where t1.mou_id = @mou_id";
                List<ENTITIES.Partner> partnerList = db.Database.SqlQuery<ENTITIES.Partner>(sql_partnerList,
                    new SqlParameter("mou_id", mou_id)).ToList();
                return partnerList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CollaborationScope> GetScopesExMOU(int mou_id)
        {
            try
            {
                string sql_scopeList = @"select * from IA_Collaboration.CollaborationScope
                    where scope_id not in (
                    select distinct t2.scope_id from IA_Collaboration.MOUPartnerScope t1 left join
                    IA_Collaboration.PartnerScope t2 on 
                    t1.partner_scope_id = t2.partner_scope_id
                    where mou_id = @mou_id and mou_bonus_id is null)";
                List<CollaborationScope> scopeList = db.Database.SqlQuery<CollaborationScope>(sql_scopeList,
                    new SqlParameter("mou_id", mou_id)).ToList();
                return scopeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CollaborationScope> GetFullScopesExMOU()
        {
            try
            {
                string sql_scopeList = @"select * from IA_Collaboration.CollaborationScope";
                List<CollaborationScope> scopeList = db.Database.SqlQuery<CollaborationScope>(sql_scopeList).ToList();
                return scopeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
