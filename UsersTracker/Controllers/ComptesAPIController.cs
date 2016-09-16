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
    public class ComptesAPIController : ApiController
    {
        private UsersTrackerContext db = new UsersTrackerContext();

        // GET: api/ComptesAPI
        public IQueryable<Compte> GetComptes()
        {
            return db.Comptes;
        }

        // GET: api/ComptesAPI/5
        [ResponseType(typeof(Compte))]
        public async Task<IHttpActionResult> GetCompte(int id)
        {
            Compte compte = await db.Comptes.FindAsync(id);
            if (compte == null)
            {
                return NotFound();
            }

            return Ok(compte);
        }

        // PUT: api/ComptesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCompte(int id, Compte compte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != compte.Id)
            {
                return BadRequest();
            }

            db.Entry(compte).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompteExists(id))
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

        // POST: api/ComptesAPI
        [ResponseType(typeof(Compte))]
        public async Task<IHttpActionResult> PostCompte(Compte compte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comptes.Add(compte);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = compte.Id }, compte);
        }

        // DELETE: api/ComptesAPI/5
        [ResponseType(typeof(Compte))]
        public async Task<IHttpActionResult> DeleteCompte(int id)
        {
            Compte compte = await db.Comptes.FindAsync(id);
            if (compte == null)
            {
                return NotFound();
            }

            db.Comptes.Remove(compte);
            await db.SaveChangesAsync();

            return Ok(compte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompteExists(int id)
        {
            return db.Comptes.Count(e => e.Id == id) > 0;
        }
    }
}