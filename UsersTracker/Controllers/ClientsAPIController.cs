using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using UsersTracker.Entities;
using UsersTracker.Models;

namespace UsersTracker.Controllers
{
    public class ClientsAPIController : ApiController
    {
        private UsersTrackerContext db = new UsersTrackerContext();

        // GET: api/ClientsAPI
        public IQueryable<Client> GetClients()
        {
            return db.Clients.Include(x => x.Compte);
        }

        // GET: api/ClientsAPI/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> GetClient(int id)
        {
            Client client = await db.Clients.Include(x => x.Compte).FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/ClientsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clients = await db.Clients.ToListAsync();

            Boolean updateOrInsert = false;
            foreach (var item in clients)
            {
                if (item.Id == client.Id)
                {
                    updateOrInsert = true;
                }
            }

            if (updateOrInsert)
            {
                db.Entry(client).State = EntityState.Modified;
            }
            else
            {
                db.Clients.Add(client);
            }

            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ClientsAPI
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = client.Id }, client);
        }

        // DELETE: api/ClientsAPI/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> DeleteClient(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            await db.SaveChangesAsync();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.Id == id) > 0;
        }
    }
}