﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User.Models;

namespace GUEST.Controllers.ScientificProducts
{
    public class ScientificProductsController : Controller
    {
        // GET: ScientificProducts
        public ActionResult Index()
        {
            ViewBag.title = "Sản phẩm khoa học";
            var pagesTree = new List<PageTree>
            {
                new PageTree("Sản phẩm khoa học","/ScientificProducts"),
            };
            ViewBag.pagesTree = pagesTree;
            return View();
        }

        public ActionResult Pending()
        {
            ViewBag.title = "Sản phẩm khoa học đang chờ phê duyệt";
            var pagesTree = new List<PageTree>
            {
                new PageTree("Sản phẩm khoa học đang chờ phê duyệt","/ScientificProducts/Pending"),
            };
            ViewBag.pagesTree = pagesTree;
            return View();
        }
    }
}