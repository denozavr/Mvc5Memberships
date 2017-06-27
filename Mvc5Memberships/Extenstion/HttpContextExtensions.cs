using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Mvc5Memberships.Extenstion
{
    public static class HttpContextExtensions
    {
        private const string Nameidentifier =
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public static string GetUserIdCtx(this HttpContextBase ctx)
        {
            var uid = string.Empty;
            try
            {
                var claims = ctx.GetOwinContext()
                    .Get<ApplicationSignInManager>()
                    .AuthenticationManager.User.Claims.FirstOrDefault(claim => claim.Type == Nameidentifier);

                // Check that the user is logged in and a claim exist
                if (claims != default(Claim))
                    uid = claims.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return uid;
        }
    }
}