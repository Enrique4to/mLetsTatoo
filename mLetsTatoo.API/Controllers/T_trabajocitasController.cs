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
    public class T_trabajocitasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_trabajocitas
        public IQueryable<T_trabajocitas> GetT_trabajocitas()
        {
            return db.T_trabajocitas;
        }

        // GET: api/T_trabajocitas/5
        [ResponseType(typeof(T_trabajocitas))]
        public async Task<IHttpActionResult> GetT_trabajocitas(int id)
        {
            T_trabajocitas t_trabajocitas = await db.T_trabajocitas.FindAsync(id);
            if (t_trabajocitas == null)
            {
                return NotFound();
            }

            return Ok(t_trabajocitas);
        }

        // PUT: api/T_trabajocitas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_trabajocitas(int id, T_trabajocitas t_trabajocitas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_trabajocitas.Id_Cita)
            {
                return BadRequest();
            }

            db.Entry(t_trabajocitas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_trabajocitasExists(id))
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

        // POST: api/T_trabajocitas
        [ResponseType(typeof(T_trabajocitas))]
        public async Task<IHttpActionResult> PostT_trabajocitas(T_trabajocitas t_trabajocitas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_trabajocitas.Add(t_trabajocitas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_trabajocitas.Id_Cita }, t_trabajocitas);
        }

        // DELETE: api/T_trabajocitas/5
        [ResponseType(typeof(T_trabajocitas))]
        public async Task<IHttpActionResult> DeleteT_trabajocitas(int id)
        {
            T_trabajocitas t_trabajocitas = await db.T_trabajocitas.FindAsync(id);
            if (t_trabajocitas == null)
            {
                return NotFound();
            }

            db.T_trabajocitas.Remove(t_trabajocitas);
            await db.SaveChangesAsync();

            return Ok(t_trabajocitas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_trabajocitasExists(int id)
        {
            return db.T_trabajocitas.Count(e => e.Id_Cita == id) > 0;
        }
    }
}