﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Mvc5Memberships.Extenstion;

namespace Mvc5Memberships.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userId = Request.IsAuthenticated ? HttpContext.GetUserIdCtx() : null;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}