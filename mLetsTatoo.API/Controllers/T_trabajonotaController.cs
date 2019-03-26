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
    public class T_trabajonotaController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_trabajonota
        public IQueryable<T_trabajonota> GetT_trabajonota()
        {
            return db.T_trabajonota;
        }

        // GET: api/T_trabajonota/5
        [ResponseType(typeof(T_trabajonota))]
        public async Task<IHttpActionResult> GetT_trabajonota(int id)
        {
            T_trabajonota t_trabajonota = await db.T_trabajonota.FindAsync(id);
            if (t_trabajonota == null)
            {
                return NotFound();
            }

            return Ok(t_trabajonota);
        }

        // PUT: api/T_trabajonota/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_trabajonota(int id, T_trabajonota t_trabajonota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_trabajonota.Id_Nota)
            {
                return BadRequest();
            }

            db.Entry(t_trabajonota).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_trabajonotaExists(id))
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

        // POST: api/T_trabajonota
        [ResponseType(typeof(T_trabajonota))]
        public async Task<IHttpActionResult> PostT_trabajonota(T_trabajonota t_trabajonota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_trabajonota.Add(t_trabajonota);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_trabajonota.Id_Nota }, t_trabajonota);
        }

        // DELETE: api/T_trabajonota/5
        [ResponseType(typeof(T_trabajonota))]
        public async Task<IHttpActionResult> DeleteT_trabajonota(int id)
        {
            T_trabajonota t_trabajonota = await db.T_trabajonota.FindAsync(id);
            if (t_trabajonota == null)
            {
                return NotFound();
            }

            db.T_trabajonota.Remove(t_trabajonota);
            await db.SaveChangesAsync();

            return Ok(t_trabajonota);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_trabajonotaExists(int id)
        {
            return db.T_trabajonota.Count(e => e.Id_Nota == id) > 0;
        }
    }
}