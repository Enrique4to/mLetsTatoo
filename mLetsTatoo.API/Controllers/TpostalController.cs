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
using mLetsTatoo.Domain.Modules;
using mLetsTatoo.Models;

namespace mLetsTatoo.API.Controllers
{
    public class TpostalController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tpostal
        public IQueryable<Tpostal> GetTpostals()
        {
            return db.Tpostals;
        }

        // GET: api/Tpostal/5
        [ResponseType(typeof(Tpostal))]
        public async Task<IHttpActionResult> GetTpostal(int id)
        {
            Tpostal tpostal = await db.Tpostals.FindAsync(id);
            if (tpostal == null)
            {
                return NotFound();
            }

            return Ok(tpostal);
        }

        // PUT: api/Tpostal/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTpostal(int id, Tpostal tpostal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tpostal.Id)
            {
                return BadRequest();
            }

            db.Entry(tpostal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TpostalExists(id))
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

        // POST: api/Tpostal
        [ResponseType(typeof(Tpostal))]
        public async Task<IHttpActionResult> PostTpostal(Tpostal tpostal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tpostals.Add(tpostal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tpostal.Id }, tpostal);
        }

        // DELETE: api/Tpostal/5
        [ResponseType(typeof(Tpostal))]
        public async Task<IHttpActionResult> DeleteTpostal(int id)
        {
            Tpostal tpostal = await db.Tpostals.FindAsync(id);
            if (tpostal == null)
            {
                return NotFound();
            }

            db.Tpostals.Remove(tpostal);
            await db.SaveChangesAsync();

            return Ok(tpostal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TpostalExists(int id)
        {
            return db.Tpostals.Count(e => e.Id == id) > 0;
        }
    }
}