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
    public class Tcita_imagenesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tcita_imagenes
        public IQueryable<Tcita_imagenes> GetTcita_imagenes()
        {
            return db.Tcita_imagenes;
        }

        // GET: api/Tcita_imagenes/5
        [ResponseType(typeof(Tcita_imagenes))]
        public async Task<IHttpActionResult> GetTcita_imagenes(int id)
        {
            Tcita_imagenes tcita_imagenes = await db.Tcita_imagenes.FindAsync(id);
            if (tcita_imagenes == null)
            {
                return NotFound();
            }

            return Ok(tcita_imagenes);
        }

        // PUT: api/Tcita_imagenes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTcita_imagenes(int id, Tcita_imagenes tcita_imagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tcita_imagenes.Id_Imagen)
            {
                return BadRequest();
            }

            db.Entry(tcita_imagenes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tcita_imagenesExists(id))
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

        // POST: api/Tcita_imagenes
        [ResponseType(typeof(Tcita_imagenes))]
        public async Task<IHttpActionResult> PostTcita_imagenes(Tcita_imagenes tcita_imagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tcita_imagenes.Add(tcita_imagenes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tcita_imagenes.Id_Imagen }, tcita_imagenes);
        }

        // DELETE: api/Tcita_imagenes/5
        [ResponseType(typeof(Tcita_imagenes))]
        public async Task<IHttpActionResult> DeleteTcita_imagenes(int id)
        {
            Tcita_imagenes tcita_imagenes = await db.Tcita_imagenes.FindAsync(id);
            if (tcita_imagenes == null)
            {
                return NotFound();
            }

            db.Tcita_imagenes.Remove(tcita_imagenes);
            await db.SaveChangesAsync();

            return Ok(tcita_imagenes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tcita_imagenesExists(int id)
        {
            return db.Tcita_imagenes.Count(e => e.Id_Imagen == id) > 0;
        }
    }
}