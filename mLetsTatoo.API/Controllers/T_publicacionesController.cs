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
    public class T_publicacionesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_publicaciones
        public IQueryable<T_publicaciones> GetT_publicaciones()
        {
            return db.T_publicaciones;
        }

        // GET: api/T_publicaciones/5
        [ResponseType(typeof(T_publicaciones))]
        public async Task<IHttpActionResult> GetT_publicaciones(int id)
        {
            T_publicaciones t_publicaciones = await db.T_publicaciones.FindAsync(id);
            if (t_publicaciones == null)
            {
                return NotFound();
            }

            return Ok(t_publicaciones);
        }

        // PUT: api/T_publicaciones/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_publicaciones(int id, T_publicaciones t_publicaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_publicaciones.Id_Publicacion)
            {
                return BadRequest();
            }

            db.Entry(t_publicaciones).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_publicacionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_publicaciones);
        }

        // POST: api/T_publicaciones
        [ResponseType(typeof(T_publicaciones))]
        public async Task<IHttpActionResult> PostT_publicaciones(T_publicaciones t_publicaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_publicaciones.Add(t_publicaciones);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_publicaciones.Id_Publicacion }, t_publicaciones);
        }

        // DELETE: api/T_publicaciones/5
        [ResponseType(typeof(T_publicaciones))]
        public async Task<IHttpActionResult> DeleteT_publicaciones(int id)
        {
            T_publicaciones t_publicaciones = await db.T_publicaciones.FindAsync(id);
            if (t_publicaciones == null)
            {
                return NotFound();
            }

            db.T_publicaciones.Remove(t_publicaciones);
            await db.SaveChangesAsync();

            return Ok(t_publicaciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_publicacionesExists(int id)
        {
            return db.T_publicaciones.Count(e => e.Id_Publicacion == id) > 0;
        }
    }
}