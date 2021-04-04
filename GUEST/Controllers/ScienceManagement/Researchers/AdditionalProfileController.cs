﻿using BLL.ModelDAL;
using BLL.ScienceManagement.Researcher;
using ENTITIES.CustomModels;
using GUEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User.Models;

namespace GUEST.Controllers
{
    public class AdditionalProfileController : Controller
    {
        private readonly List<PageTree> pagesTree = new List<PageTree>
            {
                new PageTree("Bổ sung thông tin","/AdditionalProfile"),
            };
        [HttpGet]
        public ActionResult Index()
        {
            OfficeRepo officeRepo = new OfficeRepo();
            TitleLanguageRepo titleRepo = new TitleLanguageRepo();
            CountryRepo countryRepo = new CountryRepo();

            if (CurrentAccount.Account(Session).account_id == 0)
                return Redirect("/");

            ViewBag.Titles = titleRepo.GetList(LanguageResource.GetCurrentLanguageID());
            ViewBag.Offices = officeRepo.GetList();
            ViewBag.Countries = countryRepo.GetCountries();
            ViewBag.pagesTree = pagesTree;
            return View();
        }
        [HttpPost]
        public JsonResult Add(HttpPostedFileBase identification, string person, string profile, string username)
        {
            AdditionalProfileRepo additionalProfileRepo = new AdditionalProfileRepo();
            AlertModal<int> result = additionalProfileRepo.Add(identification, person, profile, username, CurrentAccount.AccountID(Session));
            return Json(result);
        }
    }
}