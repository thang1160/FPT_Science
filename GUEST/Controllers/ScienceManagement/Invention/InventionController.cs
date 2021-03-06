﻿using BLL.ScienceManagement.Comment;
using BLL.ScienceManagement.Invention;
using BLL.ScienceManagement.MasterData;
using BLL.ScienceManagement.Paper;
using ENTITIES;
using ENTITIES.CustomModels;
using ENTITIES.CustomModels.ScienceManagement.Invention;
using ENTITIES.CustomModels.ScienceManagement.ScientificProduct;
using GUEST.Models;
using GUEST.Support;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using User.Models;

namespace GUEST.Controllers
{
    public class InventionController : Controller
    {
        private readonly InventionRepo ir = new InventionRepo();
        private readonly CommentRepo cr = new CommentRepo();
        private readonly MasterDataRepo md = new MasterDataRepo();
        private readonly ResourceManager rm = LanguageResource.GetResourceManager();

        [Auther(RightID = "28")]
        public ActionResult AddRequest()
        {
            ViewBag.title = rm.GetString("AddInventionRequest");
            var pagesTree = new List<PageTree>
            {
                new PageTree(rm.GetString("AddInventionRequest"),"/Invention/AddRequest"),
            };
            ViewBag.pagesTree = pagesTree;

            List<Country> listCountry = ir.getCountry();
            ViewBag.country = listCountry;

            List<InventionType> listType = ir.getType();
            ViewBag.type = listType;

            PaperRepo pr = new PaperRepo();
            string link = pr.GetLinkPolicy();
            ViewBag.link = link;

            return View();
        }

        //[HttpPost]
        [Auther(RightID = "28")]
        public ActionResult Edit(string id)
        {
            ViewBag.title = "Chỉnh sửa khen thưởng bằng sáng chế";
            var pagesTree = new List<PageTree>
            {
                new PageTree("Chỉnh sửa khen thưởng bằng sáng chế","/Invention/Edit"),
            };
            ViewBag.pagesTree = pagesTree;

            DetailInvention item = ir.getDetail(id);
            ViewBag.item = item;
            ViewBag.ckEdit = item.status_id;

            int request_id = item.request_id;
            //List<DetailComment> listCmt = cr.GetComment(request_id);
            //ViewBag.cmt = listCmt;
            ViewBag.id = id;

            //List<Country> listCountry = ir.getCountry();
            //ViewBag.listCountry = listCountry;
            ViewBag.request_id = request_id;

            List<InventionType> listType = ir.getType();
            ViewBag.type = listType;
            ViewBag.invenID = item.invention_id;

            List<CustomCountry> listCountry = ir.getListCountryEdit(item.invention_id);
            ViewBag.listCountry = listCountry;

            return View();
        }

        [HttpPost]
        public JsonResult ListAuthor(string id)
        {
            string lang = "";
            if (Request.Cookies["language_name"] != null)
            {
                lang = Request.Cookies["language_name"].Value;
            }
            List<AuthorInfoWithNull> listAuthor = ir.getAuthor(id, lang);
            return Json(new { author = listAuthor }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddFile(HttpPostedFileBase file, string name, string input, string type, string countries, string people)
        {
            JObject @object = JObject.Parse(input);
            JObject @object_author = JObject.Parse(people);
            JObject @object_copuntry = JObject.Parse(countries);

            Invention inven = new Invention
            {
                name = (string)@object["name"],
                no = (string)@object["no"]
            };
            DateTime temp_date = DateTime.ParseExact((string)@object["date"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
            inven.date = temp_date;

            List<Country> listCountry = new List<Country>();
            foreach (var item in @object_copuntry["countries"])
            {
                Country temp = new Country()
                {
                    country_id = (int)item["country_id"]
                };
                listCountry.Add(temp);
            }

            List<AddAuthor> author = new List<AddAuthor>();
            foreach (var item in @object_author["people"])
            {
                AddAuthor temp = new AddAuthor()
                {
                    name = (string)item["name"],
                    email = (string)item["email"],
                };
                if ((int)item["office_id"] != 0)
                {
                    temp.bank_number = (string)item["bank_number"].ToString();
                    temp.bank_branch = (string)item["bank_branch"];
                    temp.tax_code = (string)item["tax_code"].ToString();
                    temp.identification_number = (string)item["identification_number"];
                    temp.mssv_msnv = (string)item["mssv_msnv"];
                    temp.office_id = (int)item["office_id"];
                    temp.contract_id = 1;
                    temp.title_id = (int)item["title_id"];
                    temp.is_reseacher = (bool)item["is_reseacher"];
                    temp.identification_file_link = (string)item["identification_file_link"];
                }
                author.Add(temp);
            }

            Account acc = CurrentAccount.Account(Session);
            Google.Apis.Drive.v3.Data.File f = GoogleDriveService.UploadResearcherFile(file, name, 3, acc.email);
            File fl = new File
            {
                link = f.WebViewLink,
                file_drive_id = f.Id,
                name = name
            };
            _ = ir.addFile(fl);
            inven.file_id = fl.file_id;

            int id = ir.addInven_refactor(inven, type, listCountry, author, fl, acc);
            bool mess = true;
            if (id == 0) mess = false;
            //PaperRepo pr = new PaperRepo();

            //InventionType ip = ir.addInvenType(type);
            //inven.type_id = ip.invention_type_id;
            //Invention i = ir.addInven(inven);

            //BaseRequest b = pr.addBaseRequest(acc.account_id);
            //if (mess == "ss") mess = ir.addInvenRequest(b, i);
            return Json(new { mess, id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddInven(List<AddAuthor> people, Invention inven, string type)
        {
            PaperRepo pr = new PaperRepo();

            InventionType ip = ir.addInvenType(type);
            inven.type_id = ip.invention_type_id;
            Invention i = ir.addInven(inven);

            string mess = ir.addAuthor(people, i.invention_id);

            BaseRequest b = pr.AddBaseRequest(CurrentAccount.AccountID(Session));
            if (mess == "ss") mess = ir.addInvenRequest(b, i);

            return Json(new { mess, id = i.invention_id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddAuthor(List<AddAuthor> people, int invention_id)
        {
            string mess = ir.addAuthor(people, invention_id);
            return Json(new { mess }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditInven(List<AddAuthor> people, Invention inven, string type, string kieuthuong, string request)
        {
            InventionType ip = ir.addInvenType(type);
            inven.type_id = ip.invention_type_id;

            string mess = ir.editInven(inven);
            if (mess == "ss") mess = ir.editRequest(request, kieuthuong);
            if (mess == "ss")
            {
                mess = ir.updateAuthor(inven.invention_id, people);
            }

            return Json(new { mess, id = inven.invention_id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteFile(string id)
        {
            string mess = ir.deleteFileCM(id);
            return Json(new { mess }, JsonRequestBehavior.AllowGet);
        }
    }
}