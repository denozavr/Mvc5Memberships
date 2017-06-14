using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Mvc5Memberships.Models;

namespace Mvc5Memberships.Extenstion
{
    public static class IdentityExtensions
    {
        public static string GetUserFirstName(this IIdentity identity)
        {
            var db = ApplicationDbContext.Create();
            var userName = db.Users.FirstOrDefault(u => u.UserName == identity.Name)?.FirstName;

            return userName ?? string.Empty;
        }

        public static async Task GetUsers(this List<UserViewModel> users)
        {
            var db = ApplicationDbContext.Create();
            users.AddRange(await db.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName
            }).OrderBy(u => u.Email).ToListAsync());

        }
    }
}