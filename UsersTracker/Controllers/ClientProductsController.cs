using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UsersTracker.Entities;
using UsersTracker.Models;

namespace UsersTracker.Controllers
{
    public class ClientProductsController : Controller
    {
        private UsersTrackerContext db = new UsersTrackerContext();

        // GET: ClientProducts
        public async Task<ActionResult> Index()
        {
            var clientProducts = db.ClientProducts.Include(c => c.Client).Include(c => c.Product);
            return View(await clientProducts.ToListAsync());
        }

        // GET: ClientProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientProduct clientProduct = await db.ClientProducts.FindAsync(id);
            if (clientProduct == null)
            {
                return HttpNotFound();
            }
            return View(clientProduct);
        }

        // GET: ClientProducts/Create
        public ActionResult Create()
        {
            ViewBag.IdClient = new SelectList(db.Clients, "Id", "Firstname");
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name");
            ClientProduct cp = new ClientProduct();
            cp.BuyAt = DateTime.Now;
            return View(cp);
        }

        // POST: ClientProducts/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdClient,IdProduct,Number,BuyAt")] ClientProduct clientProduct)
        {
            if (ModelState.IsValid)
            {
                clientProduct.Client = await db.Clients.Include(x => x.Compte).FirstOrDefaultAsync(x => x.Id == clientProduct.IdClient);
                clientProduct.Product = await db.Products.Include(x => x.Enseigne).FirstOrDefaultAsync(x => x.Id == clientProduct.IdProduct);

                clientProduct.Client.Compte.Solde -= clientProduct.Product.Price * clientProduct.Number;

                if (clientProduct.Client.Compte.Solde >= 0)
                {
                    db.Entry(clientProduct.Client.Compte).State = EntityState.Modified;
                    db.ClientProducts.Add(clientProduct);
                    await db.SaveChangesAsync();

                    ClientEnseigne cliEn = new ClientEnseigne();
                    cliEn.Client = clientProduct.Client;
                    cliEn.IdClient = clientProduct.Client.Id;
                    cliEn.Enseigne = clientProduct.Product.Enseigne;
                    cliEn.IdEnseigne = clientProduct.Product.Enseigne.Id;
                    db.ClientEnseignes.Add(cliEn);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Vous ne possédez pas les fond nécessaire";
                }
                
            }

            ViewBag.IdClient = new SelectList(db.Clients, "Id", "Firstname", clientProduct.IdClient);
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name", clientProduct.IdProduct);
            return View(clientProduct);
        }

        // GET: ClientProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientProduct clientProduct = await db.ClientProducts.FindAsync(id);
            if (clientProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdClient = new SelectList(db.Clients, "Id", "Firstname", clientProduct.IdClient);
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name", clientProduct.IdProduct);
            return View(clientProduct);
        }

        // POST: ClientProducts/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdClient,IdProduct,Number,BuyAt")] ClientProduct clientProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdClient = new SelectList(db.Clients, "Id", "Firstname", clientProduct.IdClient);
            ViewBag.IdProduct = new SelectList(db.Products, "Id", "Name", clientProduct.IdProduct);
            return View(clientProduct);
        }

        // GET: ClientProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientProduct clientProduct = await db.ClientProducts.FindAsync(id);
            if (clientProduct == null)
            {
                return HttpNotFound();
            }
            return View(clientProduct);
        }

        // POST: ClientProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClientProduct clientProduct = await db.ClientProducts.FindAsync(id);
            db.ClientProducts.Remove(clientProduct);
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
