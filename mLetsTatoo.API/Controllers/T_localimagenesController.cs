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
    public class T_localimagenesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_localimagenes
        public IQueryable<T_localimagenes> GetT_localimagenes()
        {
            return db.T_localimagenes;
        }

        // GET: api/T_localimagenes/5
        [ResponseType(typeof(T_localimagenes))]
        public async Task<IHttpActionResult> GetT_localimagenes(int id)
        {
            T_localimagenes t_localimagenes = await db.T_localimagenes.FindAsync(id);
            if (t_localimagenes == null)
            {
                return NotFound();
            }

            return Ok(t_localimagenes);
        }

        // PUT: api/T_localimagenes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_localimagenes(int id, T_localimagenes t_localimagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_localimagenes.Id_Imagen)
            {
                return BadRequest();
            }

            db.Entry(t_localimagenes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_localimagenesExists(id))
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

        // POST: api/T_localimagenes
        [ResponseType(typeof(T_localimagenes))]
        public async Task<IHttpActionResult> PostT_localimagenes(T_localimagenes t_localimagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_localimagenes.Add(t_localimagenes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_localimagenes.Id_Imagen }, t_localimagenes);
        }

        // DELETE: api/T_localimagenes/5
        [ResponseType(typeof(T_localimagenes))]
        public async Task<IHttpActionResult> DeleteT_localimagenes(int id)
        {
            T_localimagenes t_localimagenes = await db.T_localimagenes.FindAsync(id);
            if (t_localimagenes == null)
            {
                return NotFound();
            }

            db.T_localimagenes.Remove(t_localimagenes);
            await db.SaveChangesAsync();

            return Ok(t_localimagenes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_localimagenesExists(int id)
        {
            return db.T_localimagenes.Count(e => e.Id_Imagen == id) > 0;
        }
    }
}