﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.InternationalCollaboration.Collaboration.MemorandumOfUnderstanding;
using BLL.InternationalCollaboration.MasterData;
using ENTITIES;
using ENTITIES.CustomModels;
using ENTITIES.CustomModels.InternationalCollaboration.Collaboration.MemorandumOfUnderstanding.MOU;
using Newtonsoft.Json;

namespace MANAGER.Controllers.InternationalCollaboration.MasterData
{
    public class AcademicActivityTypeController : Controller
    {
        private static AcademicActivityTypeRepo academicActivityTypeRepo = new AcademicActivityTypeRepo();

        // GET: AcademicActivityType
        public ActionResult List()
        {
            ViewBag.pageTitle = "QUẢN LÝ LOẠI HOẠT ĐỘNG HỌC THUẬT";
            return View();
        }

        public ActionResult listAcademicActivityType()
        {
            try
            {
                BaseDatatable baseDatatable = new BaseDatatable(Request);
                BaseServerSideData<AcademicActivityType> academicActivityTypes = academicActivityTypeRepo.getlistAcademicActivityType(baseDatatable);
                return Json(new
                {
                    success = true,
                    data = academicActivityTypes.Data,
                    draw = Request["draw"],
                    recordsTotal = academicActivityTypes.RecordsTotal,
                    recordsFiltered = academicActivityTypes.RecordsTotal
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult addAcademicActivityType(string academic_activity_type_name)
        {
            try
            {
                AlertModal<AcademicActivityType> alertModal = academicActivityTypeRepo.addAcademicActivityType(academic_activity_type_name);
                return Json(new { alertModal.success, alertModal.title, alertModal.content });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult getAcademicActivityType(int academic_activity_type_id)
        {
            try
            {
                AlertModal<AcademicActivityType> alertModal = academicActivityTypeRepo.getAcademicActivityType(academic_activity_type_id);
                return Json(new { alertModal.obj, alertModal.success, alertModal.title, alertModal.content });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult editAcademicActivty(int academic_activity_type_id, string academic_activity_type_name)
        {
            try
            {
                AlertModal<AcademicActivityType> alertModal = academicActivityTypeRepo.editAcademicActivityType(academic_activity_type_id, academic_activity_type_name);
                return Json(new { alertModal.success, alertModal.title, alertModal.content });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult deleteAcademicActivityType(int academic_activity_type_id)
        {
            try
            {
                AlertModal<AcademicActivityType> alertModal = academicActivityTypeRepo.deleteAcademicActivityType(academic_activity_type_id);
                return Json(new { alertModal.success, alertModal.title, alertModal.content });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
