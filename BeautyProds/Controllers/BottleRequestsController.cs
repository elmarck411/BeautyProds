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
using BeautyProds;
using BeautyProds.Models;

namespace BeautyProds.Controllers
{
    public class BottleRequestsController : ApiController
    {
        private BeautyProdsEntities db = new BeautyProdsEntities();

        // GET: api/BottleRequests
        public IQueryable<BottleRequest> GetBottleRequests()
        {
            var bottleRequests = db.BottleRequests.Include(br => br.Vendor).Include(br => br.Bottle);
            return bottleRequests;
        }

        // GET: api/BottleRequests/5
        [ResponseType(typeof(BottleRequest))]
        public async Task<IHttpActionResult> GetBottleRequest(int id)
        {
            BottleRequest bottleRequest = await db.BottleRequests.FindAsync(id);
            if (bottleRequest == null)
            {
                return NotFound();
            }

            return Ok(bottleRequest);
        }

        // PUT: api/BottleRequests/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBottleRequest(int id, BottleRequest bottleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bottleRequest.ID)
            {
                return BadRequest();
            }

            db.Entry(bottleRequest).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BottleRequestExists(id))
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

        // POST: api/BottleRequests
        [ResponseType(typeof(BottleRequest))]
        public async Task<IHttpActionResult> PostBottleRequest(BottleRequest bottleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BottleRequests.Add(bottleRequest);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bottleRequest.ID }, bottleRequest);
        }

        // DELETE: api/BottleRequests/5
        [ResponseType(typeof(BottleRequest))]
        public async Task<IHttpActionResult> DeleteBottleRequest(int id)
        {
            BottleRequest bottleRequest = await db.BottleRequests.FindAsync(id);
            if (bottleRequest == null)
            {
                return NotFound();
            }

            db.BottleRequests.Remove(bottleRequest);
            await db.SaveChangesAsync();

            return Ok(bottleRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BottleRequestExists(int id)
        {
            return db.BottleRequests.Count(e => e.ID == id) > 0;
        }
    }
}