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
    public class TtrabajosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Ttrabajos
        public IQueryable<Ttrabajos> GetTtrabajos()
        {
            return db.Ttrabajos;
        }

        // GET: api/Ttrabajos/5
        [ResponseType(typeof(Ttrabajos))]
        public async Task<IHttpActionResult> GetTtrabajos(int id)
        {
            Ttrabajos ttrabajos = await db.Ttrabajos.FindAsync(id);
            if (ttrabajos == null)
            {
                return NotFound();
            }

            return Ok(ttrabajos);
        }

        // PUT: api/Ttrabajos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTtrabajos(int id, Ttrabajos ttrabajos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ttrabajos.Id_Trabajo)
            {
                return BadRequest();
            }

            db.Entry(ttrabajos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TtrabajosExists(id))
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

        // POST: api/Ttrabajos
        [ResponseType(typeof(Ttrabajos))]
        public async Task<IHttpActionResult> PostTtrabajos(Ttrabajos ttrabajos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ttrabajos.Add(ttrabajos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ttrabajos.Id_Trabajo }, ttrabajos);
        }

        // DELETE: api/Ttrabajos/5
        [ResponseType(typeof(Ttrabajos))]
        public async Task<IHttpActionResult> DeleteTtrabajos(int id)
        {
            Ttrabajos ttrabajos = await db.Ttrabajos.FindAsync(id);
            if (ttrabajos == null)
            {
                return NotFound();
            }

            db.Ttrabajos.Remove(ttrabajos);
            await db.SaveChangesAsync();

            return Ok(ttrabajos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TtrabajosExists(int id)
        {
            return db.Ttrabajos.Count(e => e.Id_Trabajo == id) > 0;
        }
    }
}