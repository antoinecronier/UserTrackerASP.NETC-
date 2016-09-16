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
    public class EnseignesAPIController : ApiController
    {
        private UsersTrackerContext db = new UsersTrackerContext();

        // GET: api/EnseignesAPI
        public IQueryable<Enseigne> GetEnseignes()
        {
            return db.Enseignes.Include(x => x.Compte);
        }

        // GET: api/EnseignesAPI/5
        [ResponseType(typeof(Enseigne))]
        public async Task<IHttpActionResult> GetEnseigne(int id)
        {
            Enseigne enseigne = await db.Enseignes.Include(x => x.Compte).FirstOrDefaultAsync(x => x.Id == id);
            if (enseigne == null)
            {
                return NotFound();
            }

            return Ok(enseigne);
        }

        // PUT: api/EnseignesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEnseigne(int id, Enseigne enseigne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enseigne.Id)
            {
                return BadRequest();
            }

            db.Entry(enseigne).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnseigneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EnseignesAPI
        [ResponseType(typeof(Enseigne))]
        public async Task<IHttpActionResult> PostEnseigne(Enseigne enseigne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Enseignes.Add(enseigne);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = enseigne.Id }, enseigne);
        }

        // DELETE: api/EnseignesAPI/5
        [ResponseType(typeof(Enseigne))]
        public async Task<IHttpActionResult> DeleteEnseigne(int id)
        {
            Enseigne enseigne = await db.Enseignes.FindAsync(id);
            if (enseigne == null)
            {
                return NotFound();
            }

            db.Enseignes.Remove(enseigne);
            await db.SaveChangesAsync();

            return Ok(enseigne);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnseigneExists(int id)
        {
            return db.Enseignes.Count(e => e.Id == id) > 0;
        }
    }
}