﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENTITIES;

namespace ADMIN.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult List()
        {
            ViewBag.Title = "Quản lí hệ thống";
            return View();
        }
    }
}