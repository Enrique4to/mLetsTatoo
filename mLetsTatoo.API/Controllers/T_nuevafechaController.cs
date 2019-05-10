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
    public class T_nuevafechaController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_nuevafecha
        public IQueryable<T_nuevafecha> GetT_nuevafecha()
        {
            return db.T_nuevafecha;
        }

        // GET: api/T_nuevafecha/5
        [ResponseType(typeof(T_nuevafecha))]
        public async Task<IHttpActionResult> GetT_nuevafecha(int id)
        {
            T_nuevafecha t_nuevafecha = await db.T_nuevafecha.FindAsync(id);
            if (t_nuevafecha == null)
            {
                return NotFound();
            }

            return Ok(t_nuevafecha);
        }

        // PUT: api/T_nuevafecha/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_nuevafecha(int id, T_nuevafecha t_nuevafecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_nuevafecha.Id_nuevafecha)
            {
                return BadRequest();
            }

            db.Entry(t_nuevafecha).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_nuevafechaExists(id))
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

        // POST: api/T_nuevafecha
        [ResponseType(typeof(T_nuevafecha))]
        public async Task<IHttpActionResult> PostT_nuevafecha(T_nuevafecha t_nuevafecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_nuevafecha.Add(t_nuevafecha);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_nuevafecha.Id_nuevafecha }, t_nuevafecha);
        }

        // DELETE: api/T_nuevafecha/5
        [ResponseType(typeof(T_nuevafecha))]
        public async Task<IHttpActionResult> DeleteT_nuevafecha(int id)
        {
            T_nuevafecha t_nuevafecha = await db.T_nuevafecha.FindAsync(id);
            if (t_nuevafecha == null)
            {
                return NotFound();
            }

            db.T_nuevafecha.Remove(t_nuevafecha);
            await db.SaveChangesAsync();

            return Ok(t_nuevafecha);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_nuevafechaExists(int id)
        {
            return db.T_nuevafecha.Count(e => e.Id_nuevafecha == id) > 0;
        }
    }
}