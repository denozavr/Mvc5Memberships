using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Mvc5Memberships.Comparers;
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


        public static async Task<IEnumerable<ThumbnailModel>> GetProductThumbnailsAsync(
            this List<ThumbnailModel> thumbnails, string userId = null, ApplicationDbContext db = null)
        {
            try
            {
                if (userId == null) return new List<ThumbnailModel>();
                if (db == null) db = ApplicationDbContext.Create();

                var subscriptionIds = await GetSubscriptionIdsAsync(userId, db);

                thumbnails = await db.SubscriptionProducts.Join(db.Products, sp => sp.ProductId, p => p.Id,(sp, p) => new {sp,p})
                            .Join(db.ProductLinkTexts, p2 => p2.p.ProductLinkTextId, plt => plt.Id, (p2,plt)=>new{p2,plt})
                            .Join(db.ProductTypes, p3=> p3.p2.p.ProductTypeId, pt=>pt.Id, (p3,pt)=> new {p3,pt})
                            .Where(c=> subscriptionIds.Contains(c.p3.p2.sp.SubscriptionId))
                            .Select( s=> new ThumbnailModel()
                            {
                                ProductId = s.p3.p2.p.Id,
                                SubscriptionId = s.p3.p2.sp.SubscriptionId,
                                Title = s.p3.p2.p.Title,
                                Description = s.p3.p2.p.Description,
                                ImageUrl = s.p3.p2.p.ImageUrl,
                                Link = "/ProductContent/Index/" + s.p3.p2.p.Id,
                                TagText = s.p3.plt.Title,
                                ContentTag = s.pt.Title
                            }).ToListAsync();


                //SQL syntax
                //thumbnails = await (
                //    from ps in db.SubscriptionProducts
                //    join p in db.Products on ps.ProductId equals p.Id
                //    join plt in db.ProductLinkTexts on p.ProductLinkTextId equals plt.Id
                //    join pt in db.ProductTypes on p.ProductTypeId equals pt.Id
                //    where subscriptionIds.Contains(ps.SubscriptionId)
                //    select new ThumbnailModel
                //    {
                //        ProductId = p.Id,
                //        SubscriptionId = ps.SubscriptionId,
                //        Title = p.Title,
                //        Description = p.Description,
                //        ImageUrl = p.ImageUrl,
                //        Link = "/ProductContent/Index/" + p.Id,
                //        TagText = plt.Title,
                //        ContentTag = pt.Title
                //    }).ToListAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return thumbnails.Distinct(new ThumbnailEqualityComparer()).OrderBy(o => o.Title);
        }
    }
}