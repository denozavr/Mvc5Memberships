using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc5Memberships.Areas.Admin.Models;
using Mvc5Memberships.Entities;
using Mvc5Memberships.Extenstion;
using Mvc5Memberships.Models;

namespace Mvc5Memberships.Areas.Admin.Controllers
{
    public class ProductItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductItem
        public async Task<ActionResult> Index()
        {
            return View(await db.ProductItems.Convert(db));
        }

        // GET: Admin/ProductItem/Details/5
        public async Task<ActionResult> Details(int? itemId, int? productId)
        {
            if (itemId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await GetProductItem(itemId, productId);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(await productItem.Convert(db,false));
        }

        // GET: Admin/ProductItem/Create
        public async Task<ActionResult> Create()
        {
            return await ReturnProductItemModelToView();
        }

        // POST: Admin/ProductItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,ItemId")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                db.ProductItems.Add(productItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return await ReturnProductItemModelToView();
        }

        // GET: Admin/ProductItem/Edit/5
        public async Task<ActionResult> Edit(int? itemId, int? productId)
        {
            if (itemId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await GetProductItem(itemId, productId);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(await productItem.Convert(db));
        }
        private async Task<ProductItem> GetProductItem(int? itemId, int? productId)
        {
            try
            {
                int itId = 0, prodId = 0;
                int.TryParse(itemId.ToString(), out itId);
                int.TryParse(productId.ToString(), out prodId);

                var prodItem = await db.ProductItems.FirstOrDefaultAsync(pi =>
                    pi.ProductId == prodId && pi.ItemId == itId);

                return prodItem;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        private async Task<ActionResult> ReturnProductItemModelToView()
        {
            var model = new ProductItemModel
            {
                Products = await db.Products.ToListAsync(),
                Items = await db.Items.ToListAsync()
            };

            return View(model);
        }

        // POST: Admin/ProductItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,ItemId,OldProductId,OldItemId")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                //Check if productItem can be changed
                var canChange = await productItem.CanChange(db);
                if (canChange)
                {
                    await productItem.Change(db);
                }
                return RedirectToAction("Index");
            }
            return await ReturnProductItemModelToView();
        }

        // GET: Admin/ProductItem/Delete/5
        public async Task<ActionResult> Delete(int? itemId, int? productId)
        {
            if (itemId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await GetProductItem(itemId,productId);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(await productItem.Convert(db,false));
        }

        // POST: Admin/ProductItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int itemId, int productId)
        {
            ProductItem productItem = await GetProductItem(itemId,productId);
            db.ProductItems.Remove(productItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
