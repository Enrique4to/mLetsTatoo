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
    public class T_citaimagenesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_citaimagenes
        public IQueryable<T_citaimagenes> GetT_citaimagenes()
        {
            return db.T_citaimagenes;
        }

        // GET: api/T_citaimagenes/5
        [ResponseType(typeof(T_citaimagenes))]
        public async Task<IHttpActionResult> GetT_citaimagenes(int id)
        {
            T_citaimagenes t_citaimagenes = await db.T_citaimagenes.FindAsync(id);
            if (t_citaimagenes == null)
            {
                return NotFound();
            }

            return Ok(t_citaimagenes);
        }

        // PUT: api/T_citaimagenes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_citaimagenes(int id, T_citaimagenes t_citaimagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_citaimagenes.Id_Imagen)
            {
                return BadRequest();
            }

            db.Entry(t_citaimagenes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_citaimagenesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_citaimagenes);
        }

        // POST: api/T_citaimagenes
        [ResponseType(typeof(T_citaimagenes))]
        public async Task<IHttpActionResult> PostT_citaimagenes(T_citaimagenes t_citaimagenes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_citaimagenes.Add(t_citaimagenes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_citaimagenes.Id_Imagen }, t_citaimagenes);
        }

        // DELETE: api/T_citaimagenes/5
        [ResponseType(typeof(T_citaimagenes))]
        public async Task<IHttpActionResult> DeleteT_citaimagenes(int id)
        {
            T_citaimagenes t_citaimagenes = await db.T_citaimagenes.FindAsync(id);
            if (t_citaimagenes == null)
            {
                return NotFound();
            }

            db.T_citaimagenes.Remove(t_citaimagenes);
            await db.SaveChangesAsync();

            return Ok(t_citaimagenes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_citaimagenesExists(int id)
        {
            return db.T_citaimagenes.Count(e => e.Id_Imagen == id) > 0;
        }
    }
}