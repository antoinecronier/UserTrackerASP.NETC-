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
    public class ComptesController : Controller
    {
        private UsersTrackerContext db = new UsersTrackerContext();

        // GET: Comptes
        public async Task<ActionResult> Index()
        {
            return View(await db.Comptes.ToListAsync());
        }

        // GET: Comptes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compte compte = await db.Comptes.FindAsync(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // GET: Comptes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comptes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Solde,Number")] Compte compte)
        {
            if (ModelState.IsValid)
            {
                db.Comptes.Add(compte);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(compte);
        }

        // GET: Comptes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compte compte = await db.Comptes.FindAsync(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // POST: Comptes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Solde,Number")] Compte compte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compte).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(compte);
        }

        // GET: Comptes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compte compte = await db.Comptes.FindAsync(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // POST: Comptes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Compte compte = await db.Comptes.FindAsync(id);
            db.Comptes.Remove(compte);
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
