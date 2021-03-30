﻿using BLL.InternationalCollaboration.Collaboration.MemorandumOfAgreement;
using ENTITIES;
using ENTITIES.CustomModels.InternationalCollaboration.Collaboration.MemorandumOfAgreement.MOABasicInfo;
using ENTITIES.CustomModels.InternationalCollaboration.Collaboration.MemorandumOfUnderstanding.MOU;
using MANAGER.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BLL.InternationalCollaboration.Collaboration.MemorandumOfAgreement.BasicInfoMOARepo;

namespace MANAGER.Controllers.InternationalCollaboration.Collaboration.MemorandumOfAgreement
{
    public class BasicInfoMOAController : Controller
    {
        // GET: BasicInfoMOA
        private static BasicInfoMOARepo moa = new BasicInfoMOARepo();
        [Auther(RightID = "7")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PassDataToMOADetail(int id)
        {
            try
            {
                Session["moa_detail_id"] = id;
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(400);
            }
        }
        public ActionResult getBasicInfo()
        {
            try
            {
                string id = Session["moa_detail_id"].ToString();
                MOABasicInfo data = moa.getBasicInfoMOA(int.Parse(id));
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(400);
            }
        }
        public ActionResult ViewExMOA()
        {
            try
            {
                string id = Session["moa_detail_id"].ToString();
                List<ExtraMOA> listExMOA = moa.listAllExtraMOA(int.Parse(id));
                return Json(new { success = true, data = listExMOA }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(400);
            }
        }
        public ActionResult editMOABasicInfo(MOABasicInfo input)
        {
            try
            {
                string id = Session["moa_detail_id"].ToString();
                moa.editMOABasicInfo(int.Parse(id), input);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(400);
            }
        }

        public ActionResult deleteExMOA(int moa_bonus_id)
        {
            try
            {
                moa.deleteExtraMOA(moa_bonus_id);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(400);
            }
        }
        public ActionResult ViewExMOADetail(int moa_bonus_id)
        {
            try
            {
                string moa_id = Session["moa_detail_id"].ToString();
                ExMOAAdd mouObj = moa.getExtraMOADetail(int.Parse(moa_id), moa_bonus_id);
                return Json(mouObj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(400);
            }
        }
        public ActionResult Add_Ex_Moa(ExMOAAdd input)
        {
            try
            {
                string id = Session["moa_detail_id"].ToString();
                moa.addExtraMOA(input, int.Parse(id));
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Edit_Ex_Moa(ExMOAAdd input)
        {
            try
            {
                moa.editExtraMOA(input);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getNewExMOAScopesList(int partner_id)
        {
            try
            {
                string moa_id = Session["moa_detail_id"].ToString();
                string mou_id = Session["mou_detail_id"].ToString();
                List<CollaborationScope> data = moa.GetScopesExMOA(int.Parse(moa_id), int.Parse(mou_id), partner_id);
                return Json(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(400);
            }
        }
    }
}