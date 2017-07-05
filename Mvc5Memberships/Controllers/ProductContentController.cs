using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Mvc5Memberships.Extenstion;

namespace Mvc5Memberships.Controllers
{
    [Authorize]
    public class ProductContentController : Controller
    {
        // GET: ProductContent
        public async Task<ActionResult> Index(int id)
        {
            var userId = Request.IsAuthenticated ? HttpContext.GetUserIdCtx() : null;
            var sections = await SectionExtensions.GetProductSectionsAsync(id, userId);


            return View(sections);
        }
    }
}