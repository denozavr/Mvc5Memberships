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
    public class SubscriptionProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SubscriptionProduct
        public async Task<ActionResult> Index()
        {
            return View(await db.SubscriptionProducts.Convert(db));
        }

        
        // GET: Admin/SubscriptionProduct/Details/5
        public async Task<ActionResult> Details(int? productId, int? subscriptionId)
        {
            if (productId == null || subscriptionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionProduct subscriptionProduct = await GetSubscriptionProduct(productId,subscriptionId);
            if (subscriptionProduct == null)
            {
                return HttpNotFound();
            }
            return View(await subscriptionProduct.Convert(db,false));
        }

        // GET: Admin/SubscriptionProduct/Create
        public async Task<ActionResult> Create()
        {
            return await ReturnSubscriptionProductModelToView();
        }

        // POST: Admin/SubscriptionProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,SubscriptionId")] SubscriptionProduct subscriptionProduct)
        {
            if (ModelState.IsValid)
            {
                db.SubscriptionProducts.Add(subscriptionProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return await ReturnSubscriptionProductModelToView();
        }

        // GET: Admin/SubscriptionProduct/Edit/5
        public async Task<ActionResult> Edit(int? productId, int? subscriptionId)
        {
            if (productId == null || subscriptionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionProduct subscriptionProduct = await GetSubscriptionProduct(productId,subscriptionId);
            if (subscriptionProduct == null)
            {
                return HttpNotFound();
            }
            return View(await subscriptionProduct.Convert(db));
        }

        // POST: Admin/SubscriptionProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,SubscriptionId,OldProductId,OldSubscriptionId")] SubscriptionProduct subscriptionProduct)
        {
            if (ModelState.IsValid)
            {
                var canChange = await subscriptionProduct.CanChange(db);
                if (canChange)
                {
                    await subscriptionProduct.Change(db);
                }
                return RedirectToAction("Index");
            }
            return await ReturnSubscriptionProductModelToView();
        }

        // GET: Admin/SubscriptionProduct/Delete/5
        public async Task<ActionResult> Delete(int? productId, int? subscriptionId)
        {
            if (productId == null || subscriptionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionProduct subscriptionProduct = await GetSubscriptionProduct(productId,subscriptionId);
            if (subscriptionProduct == null)
            {
                return HttpNotFound();
            }
            return View(await subscriptionProduct.Convert(db,false));
        }

        // POST: Admin/SubscriptionProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int productId, int subscriptionId)
        {
            SubscriptionProduct subscriptionProduct = await GetSubscriptionProduct(productId,subscriptionId);
            db.SubscriptionProducts.Remove(subscriptionProduct);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<ActionResult> ReturnSubscriptionProductModelToView()
        {
            var model = new SubscriptionProductModel()
            {
                Products = await db.Products.ToListAsync(),
                Subscriptions = await db.Subscriptions.ToListAsync()
            };

            return View(model);
        }

        public async Task<SubscriptionProduct> GetSubscriptionProduct(int? prodId, int? subscriptionId)
        {
            try
            {
                int pId = 0, sId = 0;
                int.TryParse(prodId.ToString(), out pId);
                int.TryParse(subscriptionId.ToString(), out sId);

                var subProdItem = await db.SubscriptionProducts.FirstOrDefaultAsync(
                    sp => sp.ProductId == pId && sp.SubscriptionId == sId);

                return subProdItem;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new SubscriptionProduct();
            }
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
