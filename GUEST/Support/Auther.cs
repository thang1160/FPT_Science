﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GUEST.Support
{
    public class Auther : ActionFilterAttribute, IAuthorizationFilter
    {
        public string RightID { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            BLL.Authen.LoginRepo.User u = (BLL.Authen.LoginRepo.User)filterContext.HttpContext.Session["User"];
            if (u == null)
            {
                var Url = new UrlHelper(filterContext.RequestContext);
                var url = Url.Action("Index", "Home");
                filterContext.Result = new RedirectResult(url);
            }
            else if (!u.IsValid)
            {
                filterContext.Result = new RedirectResult("/AdditionalProfile");
            }
            else
            {
                List<int> RightIDs = u.rights;
                bool Check = false;
                foreach (var r in RightID.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (RightIDs.Contains(int.Parse(r)))
                    {
                        Check = true;
                        break;
                    }
                }
                if (!Check)
                {
                    filterContext.Result = new RedirectResult("/");
                }
            }
        }
    }
}