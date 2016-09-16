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
    public class ClientProductsAPIController : ApiController
    {
        private UsersTrackerContext db = new UsersTrackerContext();

        // GET: api/ClientProductsAPI
        public IQueryable<ClientProduct> GetClientProducts()
        {
            return db.ClientProducts.Include(x => x.Client).Include(x => x.Client.Compte).Include(x => x.Product);
        }

        // GET: api/ClientProductsAPI/5
        [ResponseType(typeof(ClientProduct))]
        public async Task<IHttpActionResult> GetClientProduct(int id)
        {
            ClientProduct clientProduct = await db.ClientProducts.Include(x => x.Client).Include(x => x.Client.Compte).Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
            if (clientProduct == null)
            {
                return NotFound();
            }

            return Ok(clientProduct);
        }

        // PUT: api/ClientProductsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClientProduct(ClientProduct clientProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienProducts = await db.Products.ToListAsync();

            Boolean updateOrInsert = false;
            foreach (var item in clienProducts)
            {
                if (item.Id == clientProduct.Id)
                {
                    updateOrInsert = true;
                }
            }

            if (updateOrInsert)
            {
                db.Entry(clientProduct).State = EntityState.Modified;
            }
            else
            {
                db.ClientProducts.Add(clientProduct);
            }

            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ClientProductsAPI
        [ResponseType(typeof(ClientProduct))]
        public async Task<IHttpActionResult> PostClientProduct(ClientProduct clientProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClientProducts.Add(clientProduct);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = clientProduct.Id }, clientProduct);
        }

        // DELETE: api/ClientProductsAPI/5
        [ResponseType(typeof(ClientProduct))]
        public async Task<IHttpActionResult> DeleteClientProduct(int id)
        {
            ClientProduct clientProduct = await db.ClientProducts.FindAsync(id);
            if (clientProduct == null)
            {
                return NotFound();
            }

            db.ClientProducts.Remove(clientProduct);
            await db.SaveChangesAsync();

            return Ok(clientProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientProductExists(int id)
        {
            return db.ClientProducts.Count(e => e.Id == id) > 0;
        }
    }
}