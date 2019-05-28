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
    public class T_notificacionesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_notificaciones
        public IQueryable<T_notificaciones> GetT_notificaciones()
        {
            return db.T_notificaciones;
        }

        // GET: api/T_notificaciones/5
        [ResponseType(typeof(T_notificaciones))]
        public async Task<IHttpActionResult> GetT_notificaciones(int id)
        {
            T_notificaciones t_notificaciones = await db.T_notificaciones.FindAsync(id);
            if (t_notificaciones == null)
            {
                return NotFound();
            }

            return Ok(t_notificaciones);
        }

        // PUT: api/T_notificaciones/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_notificaciones(int id, T_notificaciones t_notificaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_notificaciones.Id_Notificacion)
            {
                return BadRequest();
            }

            db.Entry(t_notificaciones).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_notificacionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_notificaciones);
        }

        // POST: api/T_notificaciones
        [ResponseType(typeof(T_notificaciones))]
        public async Task<IHttpActionResult> PostT_notificaciones(T_notificaciones t_notificaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_notificaciones.Add(t_notificaciones);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_notificaciones.Id_Notificacion }, t_notificaciones);
        }

        // DELETE: api/T_notificaciones/5
        [ResponseType(typeof(T_notificaciones))]
        public async Task<IHttpActionResult> DeleteT_notificaciones(int id)
        {
            T_notificaciones t_notificaciones = await db.T_notificaciones.FindAsync(id);
            if (t_notificaciones == null)
            {
                return NotFound();
            }

            db.T_notificaciones.Remove(t_notificaciones);
            await db.SaveChangesAsync();

            return Ok(t_notificaciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_notificacionesExists(int id)
        {
            return db.T_notificaciones.Count(e => e.Id_Notificacion == id) > 0;
        }
    }
}