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
    public class Ttec_imagenesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Ttec_imagenes
        public IQueryable<Ttec_imagenes> GetTtec_imagenes()
        {
            return db.Ttec_imagenes;
        }

        // GET: api/Ttec_imagenes/5
        [ResponseType(typeof(Ttec_imagenes))]
        public async Task<IHttpActionResult> GetTtec_imagenes(int id)
        {
            Ttec_imagenes ttec_imagenes = await db.Ttec_imagenes.FindAsync(id);
            if (ttec_imagenes == null)
            {
                return NotFound();
            }

            return Ok(ttec_imagenes);
        }

        // PUT: api/Ttec_imagenes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTtec_imagenes(int id, Ttec_imagenes ttec_imagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ttec_imagenes.Id_Imagen)
            {
                return BadRequest();
            }

            db.Entry(ttec_imagenes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Ttec_imagenesExists(id))
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

        // POST: api/Ttec_imagenes
        [ResponseType(typeof(Ttec_imagenes))]
        public async Task<IHttpActionResult> PostTtec_imagenes(Ttec_imagenes ttec_imagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ttec_imagenes.Add(ttec_imagenes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ttec_imagenes.Id_Imagen }, ttec_imagenes);
        }

        // DELETE: api/Ttec_imagenes/5
        [ResponseType(typeof(Ttec_imagenes))]
        public async Task<IHttpActionResult> DeleteTtec_imagenes(int id)
        {
            Ttec_imagenes ttec_imagenes = await db.Ttec_imagenes.FindAsync(id);
            if (ttec_imagenes == null)
            {
                return NotFound();
            }

            db.Ttec_imagenes.Remove(ttec_imagenes);
            await db.SaveChangesAsync();

            return Ok(ttec_imagenes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Ttec_imagenesExists(int id)
        {
            return db.Ttec_imagenes.Count(e => e.Id_Imagen == id) > 0;
        }
    }
}