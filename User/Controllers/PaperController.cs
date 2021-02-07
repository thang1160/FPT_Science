﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User.Models;

namespace User.Controllers
{
    public class PaperController : Controller
    {
        // GET: ListAllPaper
        //[Route("paper/list")]
        public ActionResult ListAll()
        {
            ViewBag.title = "Sản phẩm khoa học";
            var pagesTree = new List<PageTree>
            {
                new PageTree("Sản phẩm khoa học","/Paper/ListAll"),
            };
            ViewBag.pagesTree = pagesTree;
            return View();
        }

        public ActionResult AddRequest()
        {
            ViewBag.title = "Đăng ký khen thưởng bài báo";
            var pagesTree = new List<PageTree>
            {
                new PageTree("Đăng ký khen thưởng bài báo","/Paper/AddRequest"),
            };
            ViewBag.pagesTree = pagesTree;
            return View();
        }

        public ActionResult Pending()
        {
            ViewBag.title = "Bài báo đangg xử lý";
            var pagesTree = new List<PageTree>
            {
                new PageTree("Bài báo đangg xử lý","/Paper/Pending"),
            };
            ViewBag.pagesTree = pagesTree;
            return View();
        }
    }
}