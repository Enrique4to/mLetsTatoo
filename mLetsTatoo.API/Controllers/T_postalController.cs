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
    public class T_postalController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_postal
        public IQueryable<T_postal> GetT_postal()
        {
            return db.T_postal;
        }

        // GET: api/T_postal/5
        [ResponseType(typeof(T_postal))]
        public async Task<IHttpActionResult> GetT_postal(int id)
        {
            T_postal t_postal = await db.T_postal.FindAsync(id);
            if (t_postal == null)
            {
                return NotFound();
            }

            return Ok(t_postal);
        }

        // PUT: api/T_postal/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_postal(int id, T_postal t_postal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_postal.Id)
            {
                return BadRequest();
            }

            db.Entry(t_postal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_postalExists(id))
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

        // POST: api/T_postal
        [ResponseType(typeof(T_postal))]
        public async Task<IHttpActionResult> PostT_postal(T_postal t_postal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_postal.Add(t_postal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_postal.Id }, t_postal);
        }

        // DELETE: api/T_postal/5
        [ResponseType(typeof(T_postal))]
        public async Task<IHttpActionResult> DeleteT_postal(int id)
        {
            T_postal t_postal = await db.T_postal.FindAsync(id);
            if (t_postal == null)
            {
                return NotFound();
            }

            db.T_postal.Remove(t_postal);
            await db.SaveChangesAsync();

            return Ok(t_postal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_postalExists(int id)
        {
            return db.T_postal.Count(e => e.Id == id) > 0;
        }
    }
}