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
    public class Tlocal_imagenesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tlocal_imagenes
        public IQueryable<Tlocal_imagenes> GetTlocal_imagenes()
        {
            return db.Tlocal_imagenes;
        }

        // GET: api/Tlocal_imagenes/5
        [ResponseType(typeof(Tlocal_imagenes))]
        public async Task<IHttpActionResult> GetTlocal_imagenes(int id)
        {
            Tlocal_imagenes tlocal_imagenes = await db.Tlocal_imagenes.FindAsync(id);
            if (tlocal_imagenes == null)
            {
                return NotFound();
            }

            return Ok(tlocal_imagenes);
        }

        // PUT: api/Tlocal_imagenes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTlocal_imagenes(int id, Tlocal_imagenes tlocal_imagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tlocal_imagenes.Id_Imagen)
            {
                return BadRequest();
            }

            db.Entry(tlocal_imagenes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tlocal_imagenesExists(id))
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

        // POST: api/Tlocal_imagenes
        [ResponseType(typeof(Tlocal_imagenes))]
        public async Task<IHttpActionResult> PostTlocal_imagenes(Tlocal_imagenes tlocal_imagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tlocal_imagenes.Add(tlocal_imagenes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tlocal_imagenes.Id_Imagen }, tlocal_imagenes);
        }

        // DELETE: api/Tlocal_imagenes/5
        [ResponseType(typeof(Tlocal_imagenes))]
        public async Task<IHttpActionResult> DeleteTlocal_imagenes(int id)
        {
            Tlocal_imagenes tlocal_imagenes = await db.Tlocal_imagenes.FindAsync(id);
            if (tlocal_imagenes == null)
            {
                return NotFound();
            }

            db.Tlocal_imagenes.Remove(tlocal_imagenes);
            await db.SaveChangesAsync();

            return Ok(tlocal_imagenes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tlocal_imagenesExists(int id)
        {
            return db.Tlocal_imagenes.Count(e => e.Id_Imagen == id) > 0;
        }
    }
}