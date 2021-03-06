﻿using BLL.ModelDAL;
using ENTITIES;
using ENTITIES.CustomModels;
using ENTITIES.CustomModels.Datatable;
using MANAGER.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MANAGER.Controllers
{
    public class NotificationController : Controller
    {
        private readonly NotificationRepo notificationRepo = new NotificationRepo();

        public ActionResult Index()
        {
            SubscribeLanguageRepo subscribeRepo = new SubscribeLanguageRepo();
            ViewBag.subscribes = subscribeRepo.GetSubscribes(CurrentAccount.AccountID(Session), 1);
            return View();
        }

        public ActionResult Update(List<NotificationSubscribe> subscribes)
        {
            SubscribeLanguageRepo subscribeRepo = new SubscribeLanguageRepo();
            return Json(subscribeRepo.UpdateSubscribe(subscribes, CurrentAccount.AccountID(Session)));
        }

        [AjaxOnly]
        public JsonResult List(int start)
        {
            try
            {
                BaseServerSideData<Notification> Notis = notificationRepo.List(CurrentAccount.AccountID(Session), start, 1);
                return Json(new { success = true, content = Notis.Data, unread = Notis.RecordsTotal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Json(new { success = false, content = "Không thể lấy được dữ liệu thông báo" });
            }
        }
        public ActionResult Read(int id)
        {
            return Redirect(notificationRepo.Read(id));
        }
    }
}