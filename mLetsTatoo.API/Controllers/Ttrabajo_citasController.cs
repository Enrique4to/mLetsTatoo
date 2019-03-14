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
    public class Ttrabajo_citasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Ttrabajo_citas
        public IQueryable<Ttrabajo_citas> GetTtrabajo_citas()
        {
            return db.Ttrabajo_citas;
        }

        // GET: api/Ttrabajo_citas/5
        [ResponseType(typeof(Ttrabajo_citas))]
        public async Task<IHttpActionResult> GetTtrabajo_citas(int id)
        {
            Ttrabajo_citas ttrabajo_citas = await db.Ttrabajo_citas.FindAsync(id);
            if (ttrabajo_citas == null)
            {
                return NotFound();
            }

            return Ok(ttrabajo_citas);
        }

        // PUT: api/Ttrabajo_citas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTtrabajo_citas(int id, Ttrabajo_citas ttrabajo_citas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ttrabajo_citas.Id_Cita)
            {
                return BadRequest();
            }

            db.Entry(ttrabajo_citas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Ttrabajo_citasExists(id))
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

        // POST: api/Ttrabajo_citas
        [ResponseType(typeof(Ttrabajo_citas))]
        public async Task<IHttpActionResult> PostTtrabajo_citas(Ttrabajo_citas ttrabajo_citas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ttrabajo_citas.Add(ttrabajo_citas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ttrabajo_citas.Id_Cita }, ttrabajo_citas);
        }

        // DELETE: api/Ttrabajo_citas/5
        [ResponseType(typeof(Ttrabajo_citas))]
        public async Task<IHttpActionResult> DeleteTtrabajo_citas(int id)
        {
            Ttrabajo_citas ttrabajo_citas = await db.Ttrabajo_citas.FindAsync(id);
            if (ttrabajo_citas == null)
            {
                return NotFound();
            }

            db.Ttrabajo_citas.Remove(ttrabajo_citas);
            await db.SaveChangesAsync();

            return Ok(ttrabajo_citas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Ttrabajo_citasExists(int id)
        {
            return db.Ttrabajo_citas.Count(e => e.Id_Cita == id) > 0;
        }
    }
}