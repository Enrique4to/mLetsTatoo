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
    public class T_ciudadController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_ciudad
        public IQueryable<T_ciudad> GetT_ciudad()
        {
            return db.T_ciudad;
        }

        // GET: api/T_ciudad/5
        [ResponseType(typeof(T_ciudad))]
        public async Task<IHttpActionResult> GetT_ciudad(int id)
        {
            T_ciudad t_ciudad = await db.T_ciudad.FindAsync(id);
            if (t_ciudad == null)
            {
                return NotFound();
            }

            return Ok(t_ciudad);
        }

        // PUT: api/T_ciudad/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_ciudad(int id, T_ciudad t_ciudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_ciudad.Id)
            {
                return BadRequest();
            }

            db.Entry(t_ciudad).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_ciudadExists(id))
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

        // POST: api/T_ciudad
        [ResponseType(typeof(T_ciudad))]
        public async Task<IHttpActionResult> PostT_ciudad(T_ciudad t_ciudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_ciudad.Add(t_ciudad);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_ciudad.Id }, t_ciudad);
        }

        // DELETE: api/T_ciudad/5
        [ResponseType(typeof(T_ciudad))]
        public async Task<IHttpActionResult> DeleteT_ciudad(int id)
        {
            T_ciudad t_ciudad = await db.T_ciudad.FindAsync(id);
            if (t_ciudad == null)
            {
                return NotFound();
            }

            db.T_ciudad.Remove(t_ciudad);
            await db.SaveChangesAsync();

            return Ok(t_ciudad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_ciudadExists(int id)
        {
            return db.T_ciudad.Count(e => e.Id == id) > 0;
        }
    }
}