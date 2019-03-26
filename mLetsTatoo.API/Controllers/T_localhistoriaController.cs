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
    public class T_localhistoriaController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_localhistoria
        public IQueryable<T_localhistoria> GetT_localhistoria()
        {
            return db.T_localhistoria;
        }

        // GET: api/T_localhistoria/5
        [ResponseType(typeof(T_localhistoria))]
        public async Task<IHttpActionResult> GetT_localhistoria(int id)
        {
            T_localhistoria t_localhistoria = await db.T_localhistoria.FindAsync(id);
            if (t_localhistoria == null)
            {
                return NotFound();
            }

            return Ok(t_localhistoria);
        }

        // PUT: api/T_localhistoria/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_localhistoria(int id, T_localhistoria t_localhistoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_localhistoria.Id_Historia)
            {
                return BadRequest();
            }

            db.Entry(t_localhistoria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_localhistoriaExists(id))
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

        // POST: api/T_localhistoria
        [ResponseType(typeof(T_localhistoria))]
        public async Task<IHttpActionResult> PostT_localhistoria(T_localhistoria t_localhistoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_localhistoria.Add(t_localhistoria);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_localhistoria.Id_Historia }, t_localhistoria);
        }

        // DELETE: api/T_localhistoria/5
        [ResponseType(typeof(T_localhistoria))]
        public async Task<IHttpActionResult> DeleteT_localhistoria(int id)
        {
            T_localhistoria t_localhistoria = await db.T_localhistoria.FindAsync(id);
            if (t_localhistoria == null)
            {
                return NotFound();
            }

            db.T_localhistoria.Remove(t_localhistoria);
            await db.SaveChangesAsync();

            return Ok(t_localhistoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_localhistoriaExists(int id)
        {
            return db.T_localhistoria.Count(e => e.Id_Historia == id) > 0;
        }
    }
}