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
using mLetsTatoo.Common.Models;
using mLetsTatoo.Domain.Modules;

namespace mLetsTatoo.API.Controllers
{
    public class T_teccaractController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_teccaract
        public IQueryable<T_teccaract> GetT_teccaract()
        {
            return db.T_teccaract;
        }

        // GET: api/T_teccaract/5
        [ResponseType(typeof(T_teccaract))]
        public async Task<IHttpActionResult> GetT_teccaract(int id)
        {
            T_teccaract t_teccaract = await db.T_teccaract.FindAsync(id);
            if (t_teccaract == null)
            {
                return NotFound();
            }

            return Ok(t_teccaract);
        }

        // PUT: api/T_teccaract/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_teccaract(int id, T_teccaract t_teccaract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_teccaract.Id_Caract)
            {
                return BadRequest();
            }

            db.Entry(t_teccaract).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_teccaractExists(id))
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

        // POST: api/T_teccaract
        [ResponseType(typeof(T_teccaract))]
        public async Task<IHttpActionResult> PostT_teccaract(T_teccaract t_teccaract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_teccaract.Add(t_teccaract);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_teccaract.Id_Caract }, t_teccaract);
        }

        // DELETE: api/T_teccaract/5
        [ResponseType(typeof(T_teccaract))]
        public async Task<IHttpActionResult> DeleteT_teccaract(int id)
        {
            T_teccaract t_teccaract = await db.T_teccaract.FindAsync(id);
            if (t_teccaract == null)
            {
                return NotFound();
            }

            db.T_teccaract.Remove(t_teccaract);
            await db.SaveChangesAsync();

            return Ok(t_teccaract);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_teccaractExists(int id)
        {
            return db.T_teccaract.Count(e => e.Id_Caract == id) > 0;
        }
    }
}