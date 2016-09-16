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
    public class ClientsController : Controller
    {
        private UsersTrackerContext db = new UsersTrackerContext();

        // GET: Clients
        public async Task<ActionResult> Index()
        {
            return View(await db.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = await db.Clients.Include(x => x.Compte).FirstOrDefaultAsync(x => x.Id == id);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientCompte clientCompte)
        {
            if (ModelState.IsValid)
            {
                clientCompte.Client.Compte = clientCompte.Compte;
                db.Clients.Add(clientCompte.Client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(clientCompte);
        }

        // GET: Clients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCompte clientCompte = new ClientCompte();
            clientCompte.Client = await db.Clients.FindAsync(id);
            clientCompte.Client = await db.Clients.Include(x => x.Compte).FirstOrDefaultAsync();
            if (clientCompte == null)
            {
                return HttpNotFound();
            }
            return View(clientCompte);
        }

        // POST: Clients/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClientCompte clientCompte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientCompte.Client.Compte).State = EntityState.Modified;
                await db.SaveChangesAsync();
                db.Entry(clientCompte.Client).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clientCompte);
        }

        // GET: Clients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            db.Clients.Remove(client);
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
