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
    public class T_likepublicacionController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_likepublicacion
        public IQueryable<T_likepublicacion> GetT_likepublicacion()
        {
            return db.T_likepublicacion;
        }

        // GET: api/T_likepublicacion/5
        [ResponseType(typeof(T_likepublicacion))]
        public async Task<IHttpActionResult> GetT_likepublicacion(int id)
        {
            T_likepublicacion t_likepublicacion = await db.T_likepublicacion.FindAsync(id);
            if (t_likepublicacion == null)
            {
                return NotFound();
            }

            return Ok(t_likepublicacion);
        }

        // PUT: api/T_likepublicacion/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_likepublicacion(int id, T_likepublicacion t_likepublicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_likepublicacion.Id_Likepublicacion)
            {
                return BadRequest();
            }

            db.Entry(t_likepublicacion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_likepublicacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_likepublicacion);
        }

        // POST: api/T_likepublicacion
        [ResponseType(typeof(T_likepublicacion))]
        public async Task<IHttpActionResult> PostT_likepublicacion(T_likepublicacion t_likepublicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_likepublicacion.Add(t_likepublicacion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_likepublicacion.Id_Likepublicacion }, t_likepublicacion);
        }

        // DELETE: api/T_likepublicacion/5
        [ResponseType(typeof(T_likepublicacion))]
        public async Task<IHttpActionResult> DeleteT_likepublicacion(int id)
        {
            T_likepublicacion t_likepublicacion = await db.T_likepublicacion.FindAsync(id);
            if (t_likepublicacion == null)
            {
                return NotFound();
            }

            db.T_likepublicacion.Remove(t_likepublicacion);
            await db.SaveChangesAsync();

            return Ok(t_likepublicacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_likepublicacionExists(int id)
        {
            return db.T_likepublicacion.Count(e => e.Id_Likepublicacion == id) > 0;
        }
    }
}