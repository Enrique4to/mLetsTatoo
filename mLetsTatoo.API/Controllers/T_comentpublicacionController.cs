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
    public class T_comentpublicacionController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_comentpublicacion
        public IQueryable<T_comentpublicacion> GetT_comentpublicacion()
        {
            return db.T_comentpublicacion;
        }

        // GET: api/T_comentpublicacion/5
        [ResponseType(typeof(T_comentpublicacion))]
        public async Task<IHttpActionResult> GetT_comentpublicacion(int id)
        {
            T_comentpublicacion t_comentpublicacion = await db.T_comentpublicacion.FindAsync(id);
            if (t_comentpublicacion == null)
            {
                return NotFound();
            }

            return Ok(t_comentpublicacion);
        }

        // PUT: api/T_comentpublicacion/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_comentpublicacion(int id, T_comentpublicacion t_comentpublicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_comentpublicacion.Id_Comentario)
            {
                return BadRequest();
            }

            db.Entry(t_comentpublicacion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_comentpublicacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_comentpublicacion);
        }

        // POST: api/T_comentpublicacion
        [ResponseType(typeof(T_comentpublicacion))]
        public async Task<IHttpActionResult> PostT_comentpublicacion(T_comentpublicacion t_comentpublicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_comentpublicacion.Add(t_comentpublicacion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_comentpublicacion.Id_Comentario }, t_comentpublicacion);
        }

        // DELETE: api/T_comentpublicacion/5
        [ResponseType(typeof(T_comentpublicacion))]
        public async Task<IHttpActionResult> DeleteT_comentpublicacion(int id)
        {
            T_comentpublicacion t_comentpublicacion = await db.T_comentpublicacion.FindAsync(id);
            if (t_comentpublicacion == null)
            {
                return NotFound();
            }

            db.T_comentpublicacion.Remove(t_comentpublicacion);
            await db.SaveChangesAsync();

            return Ok(t_comentpublicacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_comentpublicacionExists(int id)
        {
            return db.T_comentpublicacion.Count(e => e.Id_Comentario == id) > 0;
        }
    }
}