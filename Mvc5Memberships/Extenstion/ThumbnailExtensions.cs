using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Mvc5Memberships.Models;

namespace Mvc5Memberships.Extenstion
{
    public static class ThumbnailExtensions
    {
        private static async Task<List<int>> GetSubscriptionIdsAsync(
            string userId = null, ApplicationDbContext db = null)
        {
            try
            {
                if (userId == null) return new List<int>();
                if (db == null) db = ApplicationDbContext.Create();

                return await (db.UserSubscriptions.Where(us => us.UserId == userId)
                    .Select(us => us.SubscriptionId)).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new List<int>();
        }

    }
}