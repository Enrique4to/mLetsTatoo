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
    public class T_redesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_redes
        public IQueryable<T_redes> GetT_redes()
        {
            return db.T_redes;
        }

        // GET: api/T_redes/5
        [ResponseType(typeof(T_redes))]
        public async Task<IHttpActionResult> GetT_redes(int id)
        {
            T_redes t_redes = await db.T_redes.FindAsync(id);
            if (t_redes == null)
            {
                return NotFound();
            }

            return Ok(t_redes);
        }

        // PUT: api/T_redes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_redes(int id, T_redes t_redes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_redes.Id_Redes)
            {
                return BadRequest();
            }

            db.Entry(t_redes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_redesExists(id))
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

        // POST: api/T_redes
        [ResponseType(typeof(T_redes))]
        public async Task<IHttpActionResult> PostT_redes(T_redes t_redes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_redes.Add(t_redes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_redes.Id_Redes }, t_redes);
        }

        // DELETE: api/T_redes/5
        [ResponseType(typeof(T_redes))]
        public async Task<IHttpActionResult> DeleteT_redes(int id)
        {
            T_redes t_redes = await db.T_redes.FindAsync(id);
            if (t_redes == null)
            {
                return NotFound();
            }

            db.T_redes.Remove(t_redes);
            await db.SaveChangesAsync();

            return Ok(t_redes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_redesExists(int id)
        {
            return db.T_redes.Count(e => e.Id_Redes == id) > 0;
        }
    }
}