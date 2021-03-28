﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User.Models;
using BLL.InternationalCollaboration.AcademicCollaborationRepository;
using ENTITIES.CustomModels.InternationalCollaboration.AcademicCollaborationEntities;

namespace GUEST.Controllers.InternationalCollaboration.AcademicCollaboration
{
    public class AcademicCollaborationController : Controller
    {
        private static AcademicCollaborationGuestRepo guestRepo = new AcademicCollaborationGuestRepo();
        private System.Resources.ResourceManager rm = Models.LanguageResource.GetResourceManager();
        // GET: AcademicCollaboration
        public ActionResult Long_Term()
        {
            int language;
            if (Request.Cookies["language_id"] is null)
            {
                language = 1;
            }
            else
            {
                language = Int32.Parse(Request.Cookies["language_id"].Value);
            }
            var pagesTree = new List<PageTree>
            {
                new PageTree(rm.GetString("LongTerm"), "/AcademicCollaboration/Long_Term"),
            };
            ViewBag.Title = rm.GetString("LongTerm");
            ViewBag.TypeDescription = guestRepo.getTypeDescription(2, language);
            ViewBag.ListProgram = guestRepo.listProgram(0, 2, language);
            ViewBag.ListCountry = guestRepo.listCountry();
            ViewBag.pagesTree = pagesTree;
            return View();
        }
        [HttpPost]
        public ActionResult Load_More_List_Long_Term(int count)
        {
            int language = Int32.Parse(Request.Cookies["language_id"].Value);
            List<ProgramInfo> data = guestRepo.listProgram(count, 2, language);
            return Json(data);
        }
        [HttpPost]
        public ActionResult Partner_Program()
        {
            int language = Int32.Parse(Request.Cookies["language_id"].Value);
            List<ProgramInfo> data = guestRepo.listPartnerProgram(language);
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FPT_Program()
        {
            int language = Int32.Parse(Request.Cookies["language_id"].Value);
            List<ProgramInfo> data = guestRepo.listFPTProgram(language);
            return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Short_Term()
        {
            var pagesTree = new List<PageTree>
            {
                new PageTree("Trao đổi cán bộ giảng viên", "/AcademicCollaboration/Short_Term"),
            };
            ViewBag.pagesTree = pagesTree;
            return View();
        }

        public ActionResult Load_More_List_Short_Term()
        {
            try
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Program_Detail(string id, string type_program)
        {
            var pagesTree = new List<PageTree>();
            switch (type_program)
            {
                case "1":
                    pagesTree = new List<PageTree>
            {
                new PageTree("Đào tạo sau đại học", "/AcademicCollaboration/Long_Term"),
                new PageTree("Đào tạo của đối tác", "/AcademicCollaboration/Program_Detail"),
                //new PageTree("Đào tạo của đối tác", "/AcademicCollaboration/Program_Detail?id=" + id),
            };
                    break;
                case "2":
                    pagesTree = new List<PageTree>
            {
                new PageTree("Đào tạo sau đại học", "/AcademicCollaboration/Long_Term"),
                new PageTree("Đào tạo của FPT", "/AcademicCollaboration/Program_Detail"),
                //new PageTree("Đào tạo của FPT", "/AcademicCollaboration/Program_Detail?id=" + id),
            };
                    break;
                case "3":
                    pagesTree = new List<PageTree>
            {
                new PageTree("Trao đổi cán bộ giảng viên", "/AcademicCollaboration/Short_Term"),
                new PageTree("Trao đổi với đối tác", "/AcademicCollaboration/Program_Detail"),
                //new PageTree("Trao đổi với đối tác", "/AcademicCollaboration/Program_Detail?id=" + id),
            };
                    break;
                case "4":
                    pagesTree = new List<PageTree>
            {
                new PageTree("Trao đổi cán bộ giảng viên", "/AcademicCollaboration/Short_Term"),
                new PageTree("Trao đổi với FPT", "/AcademicCollaboration/Program_Detail"),
                //new PageTree("Trao đổi với FPT", "/AcademicCollaboration/Program_Detail?id=" + id),
            };
                    break;
            }
            ViewBag.pagesTree = pagesTree;
            return View();
        }

        public ActionResult Procedure_Detail(string id, string type_procedure)
        {
            var pagesTree = new List<PageTree>();
            switch (type_procedure)
            {
                case "1":
                    pagesTree = new List<PageTree>
            {
                new PageTree("Trao đổi cán bộ giảng viên", "/AcademicCollaboration/Short_Term"),
                new PageTree("Thủ tục với đối tác", "/AcademicCollaboration/Procedure_Detail"),
                //new PageTree("Đào tạo của đối tác", "/AcademicCollaboration/Procedure_Detail?id=" + id),
            };
                    break;
                case "2":
                    pagesTree = new List<PageTree>
            {
                new PageTree("Trao đổi cán bộ giảng viên", "/AcademicCollaboration/Short_Term"),
                new PageTree("Thủ tục với FPT", "/AcademicCollaboration/Procedure_Detail"),
                //new PageTree("Đào tạo của FPT", "/AcademicCollaboration/Procedure_Detail?id=" + id),
            };
                    break;
            }
            ViewBag.pagesTree = pagesTree;
            return View();
        }
    }
}