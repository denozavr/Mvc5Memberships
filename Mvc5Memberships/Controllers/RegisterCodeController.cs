using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Mvc5Memberships.Extenstion;

namespace Mvc5Memberships.Controllers
{
    public class RegisterCodeController : Controller
    {
        // GET: RegisterCode
        public async Task<ActionResult> Register(string code)
        {
            if (Request.IsAuthenticated)
            {
                var userId = HttpContext.GetUserIdCtx();

                var registred = await SubscriptionExtensions
                    .RegisterUserSubscriptionCode(userId, code);

                if (!registred) throw new ApplicationException();

                return PartialView("_RegisterCodePartial");
            }
            return View("Error");
        }
    }
}