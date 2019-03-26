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
    public class T_localesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_locales
        public IQueryable<T_locales> GetT_locales()
        {
            return db.T_locales;
        }

        // GET: api/T_locales/5
        [ResponseType(typeof(T_locales))]
        public async Task<IHttpActionResult> GetT_locales(int id)
        {
            T_locales t_locales = await db.T_locales.FindAsync(id);
            if (t_locales == null)
            {
                return NotFound();
            }

            return Ok(t_locales);
        }

        // PUT: api/T_locales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_locales(int id, T_locales t_locales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_locales.Id_Local)
            {
                return BadRequest();
            }

            db.Entry(t_locales).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_localesExists(id))
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

        // POST: api/T_locales
        [ResponseType(typeof(T_locales))]
        public async Task<IHttpActionResult> PostT_locales(T_locales t_locales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_locales.Add(t_locales);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_locales.Id_Local }, t_locales);
        }

        // DELETE: api/T_locales/5
        [ResponseType(typeof(T_locales))]
        public async Task<IHttpActionResult> DeleteT_locales(int id)
        {
            T_locales t_locales = await db.T_locales.FindAsync(id);
            if (t_locales == null)
            {
                return NotFound();
            }

            db.T_locales.Remove(t_locales);
            await db.SaveChangesAsync();

            return Ok(t_locales);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_localesExists(int id)
        {
            return db.T_locales.Count(e => e.Id_Local == id) > 0;
        }
    }
}