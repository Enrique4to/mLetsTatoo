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
    public class T_trabajosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_trabajos
        public IQueryable<T_trabajos> GetT_trabajos()
        {
            return db.T_trabajos;
        }

        // GET: api/T_trabajos/5
        [ResponseType(typeof(T_trabajos))]
        public async Task<IHttpActionResult> GetT_trabajos(int id)
        {
            T_trabajos t_trabajos = await db.T_trabajos.FindAsync(id);
            if (t_trabajos == null)
            {
                return NotFound();
            }

            return Ok(t_trabajos);
        }

        // PUT: api/T_trabajos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_trabajos(int id, T_trabajos t_trabajos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_trabajos.Id_Trabajo)
            {
                return BadRequest();
            }

            db.Entry(t_trabajos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_trabajosExists(id))
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

        // POST: api/T_trabajos
        [ResponseType(typeof(T_trabajos))]
        public async Task<IHttpActionResult> PostT_trabajos(T_trabajos t_trabajos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_trabajos.Add(t_trabajos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_trabajos.Id_Trabajo }, t_trabajos);
        }

        // DELETE: api/T_trabajos/5
        [ResponseType(typeof(T_trabajos))]
        public async Task<IHttpActionResult> DeleteT_trabajos(int id)
        {
            T_trabajos t_trabajos = await db.T_trabajos.FindAsync(id);
            if (t_trabajos == null)
            {
                return NotFound();
            }

            db.T_trabajos.Remove(t_trabajos);
            await db.SaveChangesAsync();

            return Ok(t_trabajos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_trabajosExists(int id)
        {
            return db.T_trabajos.Count(e => e.Id_Trabajo == id) > 0;
        }
    }
}