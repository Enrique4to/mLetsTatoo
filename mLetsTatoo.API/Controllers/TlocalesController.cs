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
    public class TlocalesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tlocales
        public IQueryable<Tlocales> GetTlocales()
        {
            return db.Tlocales;
        }

        // GET: api/Tlocales/5
        [ResponseType(typeof(Tlocales))]
        public async Task<IHttpActionResult> GetTlocales(int id)
        {
            Tlocales tlocales = await db.Tlocales.FindAsync(id);
            if (tlocales == null)
            {
                return NotFound();
            }

            return Ok(tlocales);
        }

        // PUT: api/Tlocales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTlocales(int id, Tlocales tlocales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tlocales.Id_Local)
            {
                return BadRequest();
            }

            db.Entry(tlocales).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TlocalesExists(id))
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

        // POST: api/Tlocales
        [ResponseType(typeof(Tlocales))]
        public async Task<IHttpActionResult> PostTlocales(Tlocales tlocales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tlocales.Add(tlocales);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tlocales.Id_Local }, tlocales);
        }

        // DELETE: api/Tlocales/5
        [ResponseType(typeof(Tlocales))]
        public async Task<IHttpActionResult> DeleteTlocales(int id)
        {
            Tlocales tlocales = await db.Tlocales.FindAsync(id);
            if (tlocales == null)
            {
                return NotFound();
            }

            db.Tlocales.Remove(tlocales);
            await db.SaveChangesAsync();

            return Ok(tlocales);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TlocalesExists(int id)
        {
            return db.Tlocales.Count(e => e.Id_Local == id) > 0;
        }
    }
}