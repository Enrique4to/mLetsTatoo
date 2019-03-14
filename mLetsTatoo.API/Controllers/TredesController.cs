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
    public class TredesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tredes
        public IQueryable<Tredes> GetTredes()
        {
            return db.Tredes;
        }

        // GET: api/Tredes/5
        [ResponseType(typeof(Tredes))]
        public async Task<IHttpActionResult> GetTredes(int id)
        {
            Tredes tredes = await db.Tredes.FindAsync(id);
            if (tredes == null)
            {
                return NotFound();
            }

            return Ok(tredes);
        }

        // PUT: api/Tredes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTredes(int id, Tredes tredes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tredes.Id_Redes)
            {
                return BadRequest();
            }

            db.Entry(tredes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TredesExists(id))
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

        // POST: api/Tredes
        [ResponseType(typeof(Tredes))]
        public async Task<IHttpActionResult> PostTredes(Tredes tredes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tredes.Add(tredes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tredes.Id_Redes }, tredes);
        }

        // DELETE: api/Tredes/5
        [ResponseType(typeof(Tredes))]
        public async Task<IHttpActionResult> DeleteTredes(int id)
        {
            Tredes tredes = await db.Tredes.FindAsync(id);
            if (tredes == null)
            {
                return NotFound();
            }

            db.Tredes.Remove(tredes);
            await db.SaveChangesAsync();

            return Ok(tredes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TredesExists(int id)
        {
            return db.Tredes.Count(e => e.Id_Redes == id) > 0;
        }
    }
}