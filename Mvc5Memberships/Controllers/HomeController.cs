using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Mvc5Memberships.Extenstion;
using Mvc5Memberships.Models;

namespace Mvc5Memberships.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var userId = Request.IsAuthenticated ? HttpContext.GetUserIdCtx() : null;
            var thumbs = await new List<ThumbnailModel>().GetProductThumbnailsAsync(userId);

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