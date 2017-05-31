using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Mvc5Memberships.Areas.Admin.Models;
using Mvc5Memberships.Entities;
using Mvc5Memberships.Models;

namespace Mvc5Memberships.Extenstion
{
    public static class ConversionExtensions
    {
        public static async Task<IEnumerable<ProductModel>> Convert(
            this IEnumerable<Product> products, ApplicationDbContext db)
        {
            var prods = products as IList<Product> ?? products.ToList();
            if (!prods.Any())
                return new List<ProductModel>();

            var texts = await db.ProductLinkTexts.ToListAsync();
            var types = await db.ProductTypes.ToListAsync();

            return prods.Select(x => new ProductModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                ProductLinkTextId = x.ProductLinkTextId,
                ProductTypeId = x.ProductTypeId,
                ProductLinkTexts = texts,
                ProductTypes = types
            });
        }
    }
}