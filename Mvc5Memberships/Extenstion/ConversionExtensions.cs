using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
        #region Product
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


        public static async Task<ProductModel> Convert(this Product product, ApplicationDbContext db)
        {
            if (product == null)
                return new ProductModel();

            var text = await db.ProductLinkTexts.FirstOrDefaultAsync(x => x.Id == product.ProductLinkTextId);
            var type = await db.ProductTypes.FirstOrDefaultAsync(x => x.Id == product.ProductTypeId);

            var model = new ProductModel()
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                ProductLinkTextId = product.ProductLinkTextId,
                ProductTypeId = product.ProductTypeId,
                ProductLinkTexts = new List<ProductLinkText>(),
                ProductTypes = new List<ProductType>()
            };


            model.ProductLinkTexts.Add(text);
            model.ProductTypes.Add(type);

            return model;
        }
        #endregion



        #region ProductItem
        public static async Task<IEnumerable<ProductItemModel>> Convert(
          this IQueryable<ProductItem> productItems, ApplicationDbContext db)
        {
            if (!productItems.Any())
                return new List<ProductItemModel>();

            try
            {
                var model = await productItems.Select(x => new ProductItemModel()
                {
                    ItemId = x.ItemId,
                    ProductId = x.ProductId,
                    ItemTitle = db.Items.FirstOrDefault(i => i.Id == x.ItemId).Title,
                    ProductTitle = db.Products.FirstOrDefault(p => p.Id == x.ProductId).Title
                }).ToListAsync();

                return model;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new List<ProductItemModel>();
            }


        } 
        #endregion

    }
}