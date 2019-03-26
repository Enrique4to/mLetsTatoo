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
    public class T_estadoController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_estado
        public IQueryable<T_estado> GetT_estado()
        {
            return db.T_estado;
        }

        // GET: api/T_estado/5
        [ResponseType(typeof(T_estado))]
        public async Task<IHttpActionResult> GetT_estado(int id)
        {
            T_estado t_estado = await db.T_estado.FindAsync(id);
            if (t_estado == null)
            {
                return NotFound();
            }

            return Ok(t_estado);
        }

        // PUT: api/T_estado/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_estado(int id, T_estado t_estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_estado.Id)
            {
                return BadRequest();
            }

            db.Entry(t_estado).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_estadoExists(id))
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

        // POST: api/T_estado
        [ResponseType(typeof(T_estado))]
        public async Task<IHttpActionResult> PostT_estado(T_estado t_estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_estado.Add(t_estado);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_estado.Id }, t_estado);
        }

        // DELETE: api/T_estado/5
        [ResponseType(typeof(T_estado))]
        public async Task<IHttpActionResult> DeleteT_estado(int id)
        {
            T_estado t_estado = await db.T_estado.FindAsync(id);
            if (t_estado == null)
            {
                return NotFound();
            }

            db.T_estado.Remove(t_estado);
            await db.SaveChangesAsync();

            return Ok(t_estado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_estadoExists(int id)
        {
            return db.T_estado.Count(e => e.Id == id) > 0;
        }
    }
}