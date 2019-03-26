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
    public class T_tecimagenesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_tecimagenes
        public IQueryable<T_tecimagenes> GetT_tecimagenes()
        {
            return db.T_tecimagenes;
        }

        // GET: api/T_tecimagenes/5
        [ResponseType(typeof(T_tecimagenes))]
        public async Task<IHttpActionResult> GetT_tecimagenes(int id)
        {
            T_tecimagenes t_tecimagenes = await db.T_tecimagenes.FindAsync(id);
            if (t_tecimagenes == null)
            {
                return NotFound();
            }

            return Ok(t_tecimagenes);
        }

        // PUT: api/T_tecimagenes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_tecimagenes(int id, T_tecimagenes t_tecimagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_tecimagenes.Id_Imagen)
            {
                return BadRequest();
            }

            db.Entry(t_tecimagenes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_tecimagenesExists(id))
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

        // POST: api/T_tecimagenes
        [ResponseType(typeof(T_tecimagenes))]
        public async Task<IHttpActionResult> PostT_tecimagenes(T_tecimagenes t_tecimagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_tecimagenes.Add(t_tecimagenes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_tecimagenes.Id_Imagen }, t_tecimagenes);
        }

        // DELETE: api/T_tecimagenes/5
        [ResponseType(typeof(T_tecimagenes))]
        public async Task<IHttpActionResult> DeleteT_tecimagenes(int id)
        {
            T_tecimagenes t_tecimagenes = await db.T_tecimagenes.FindAsync(id);
            if (t_tecimagenes == null)
            {
                return NotFound();
            }

            db.T_tecimagenes.Remove(t_tecimagenes);
            await db.SaveChangesAsync();

            return Ok(t_tecimagenes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_tecimagenesExists(int id)
        {
            return db.T_tecimagenes.Count(e => e.Id_Imagen == id) > 0;
        }
    }
}