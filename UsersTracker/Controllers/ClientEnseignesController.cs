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
    public class ClientEnseignesController : Controller
    {
        private UsersTrackerContext db = new UsersTrackerContext();

        // GET: ClientEnseignes
        public async Task<ActionResult> Index()
        {
            return View(await db.ClientEnseignes.ToListAsync());
        }

        // GET: ClientEnseignes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEnseigne clientEnseigne = await db.ClientEnseignes.FindAsync(id);
            if (clientEnseigne == null)
            {
                return HttpNotFound();
            }
            return View(clientEnseigne);
        }

        // GET: ClientEnseignes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientEnseignes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdClient,IdEnseigne")] ClientEnseigne clientEnseigne)
        {
            if (ModelState.IsValid)
            {
                db.ClientEnseignes.Add(clientEnseigne);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(clientEnseigne);
        }

        // GET: ClientEnseignes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEnseigne clientEnseigne = await db.ClientEnseignes.FindAsync(id);
            if (clientEnseigne == null)
            {
                return HttpNotFound();
            }
            return View(clientEnseigne);
        }

        // POST: ClientEnseignes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdClient,IdEnseigne")] ClientEnseigne clientEnseigne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientEnseigne).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clientEnseigne);
        }

        // GET: ClientEnseignes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientEnseigne clientEnseigne = await db.ClientEnseignes.FindAsync(id);
            if (clientEnseigne == null)
            {
                return HttpNotFound();
            }
            return View(clientEnseigne);
        }

        // POST: ClientEnseignes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClientEnseigne clientEnseigne = await db.ClientEnseignes.FindAsync(id);
            db.ClientEnseignes.Remove(clientEnseigne);
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
