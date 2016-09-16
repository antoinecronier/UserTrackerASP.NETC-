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
using UsersTracker.Entities.Utils;

namespace UsersTracker.Controllers
{
    public class EnseignesController : Controller
    {
        private UsersTrackerContext db = new UsersTrackerContext();

        // GET: Enseignes
        public async Task<ActionResult> Index()
        {
            return View(await db.Enseignes.ToListAsync());
        }

        // GET: Enseignes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enseigne enseigne = await db.Enseignes.Include(x => x.Compte).Include(x => x.Products.Select(y => y.Enseigne)).FirstOrDefaultAsync(x => x.Id == id);

            if (enseigne == null)
            {
                return HttpNotFound();
            }
            return View(enseigne);
        }

        // GET: Enseignes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enseignes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Entities.Utils.EnseigneCompte enseigneCompte)
        {
            if (ModelState.IsValid)
            {
                enseigneCompte.Enseigne.Compte = enseigneCompte.Compte;
                db.Enseignes.Add(enseigneCompte.Enseigne);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(enseigneCompte.Enseigne);
        }

        // GET: Enseignes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enseigne enseigne = await db.Enseignes.FindAsync(id);
            if (enseigne == null)
            {
                return HttpNotFound();
            }
            return View(enseigne);
        }

        // POST: Enseignes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Enseigne enseigne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enseigne).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(enseigne);
        }

        // GET: Enseignes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enseigne enseigne = await db.Enseignes.FindAsync(id);
            if (enseigne == null)
            {
                return HttpNotFound();
            }
            return View(enseigne);
        }

        // POST: Enseignes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enseigne enseigne = await db.Enseignes.Include(x => x.Compte).Include(x => x.Products.Select(y => y.Enseigne)).FirstOrDefaultAsync(x => x.Id == id);
            db.Comptes.Remove(enseigne.Compte);
            db.Products.RemoveRange(enseigne.Products);
            db.Enseignes.Remove(enseigne);
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
