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
    public class TciudadController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tciudad
        public IQueryable<Tciudad> GetTciudads()
        {
            return db.Tciudads;
        }

        // GET: api/Tciudad/5
        [ResponseType(typeof(Tciudad))]
        public async Task<IHttpActionResult> GetTciudad(int id)
        {
            Tciudad tciudad = await db.Tciudads.FindAsync(id);
            if (tciudad == null)
            {
                return NotFound();
            }

            return Ok(tciudad);
        }

        // PUT: api/Tciudad/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTciudad(int id, Tciudad tciudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tciudad.Id)
            {
                return BadRequest();
            }

            db.Entry(tciudad).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TciudadExists(id))
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

        // POST: api/Tciudad
        [ResponseType(typeof(Tciudad))]
        public async Task<IHttpActionResult> PostTciudad(Tciudad tciudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tciudads.Add(tciudad);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tciudad.Id }, tciudad);
        }

        // DELETE: api/Tciudad/5
        [ResponseType(typeof(Tciudad))]
        public async Task<IHttpActionResult> DeleteTciudad(int id)
        {
            Tciudad tciudad = await db.Tciudads.FindAsync(id);
            if (tciudad == null)
            {
                return NotFound();
            }

            db.Tciudads.Remove(tciudad);
            await db.SaveChangesAsync();

            return Ok(tciudad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TciudadExists(int id)
        {
            return db.Tciudads.Count(e => e.Id == id) > 0;
        }
    }
}