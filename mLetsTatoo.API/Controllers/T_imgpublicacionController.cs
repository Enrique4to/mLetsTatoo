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
    public class T_imgpublicacionController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_imgpublicacion
        public IQueryable<T_imgpublicacion> GetT_imgpublicacion()
        {
            return db.T_imgpublicacion;
        }

        // GET: api/T_imgpublicacion/5
        [ResponseType(typeof(T_imgpublicacion))]
        public async Task<IHttpActionResult> GetT_imgpublicacion(int id)
        {
            T_imgpublicacion t_imgpublicacion = await db.T_imgpublicacion.FindAsync(id);
            if (t_imgpublicacion == null)
            {
                return NotFound();
            }

            return Ok(t_imgpublicacion);
        }

        // PUT: api/T_imgpublicacion/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_imgpublicacion(int id, T_imgpublicacion t_imgpublicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_imgpublicacion.Id_Imgpublicacion)
            {
                return BadRequest();
            }

            db.Entry(t_imgpublicacion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_imgpublicacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_imgpublicacion);
        }

        // POST: api/T_imgpublicacion
        [ResponseType(typeof(T_imgpublicacion))]
        public async Task<IHttpActionResult> PostT_imgpublicacion(T_imgpublicacion t_imgpublicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_imgpublicacion.Add(t_imgpublicacion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_imgpublicacion.Id_Imgpublicacion }, t_imgpublicacion);
        }

        // DELETE: api/T_imgpublicacion/5
        [ResponseType(typeof(T_imgpublicacion))]
        public async Task<IHttpActionResult> DeleteT_imgpublicacion(int id)
        {
            T_imgpublicacion t_imgpublicacion = await db.T_imgpublicacion.FindAsync(id);
            if (t_imgpublicacion == null)
            {
                return NotFound();
            }

            db.T_imgpublicacion.Remove(t_imgpublicacion);
            await db.SaveChangesAsync();

            return Ok(t_imgpublicacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_imgpublicacionExists(int id)
        {
            return db.T_imgpublicacion.Count(e => e.Id_Imgpublicacion == id) > 0;
        }
    }
}