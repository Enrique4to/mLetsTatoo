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
    public class T_notif_citasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_notif_citas
        public IQueryable<T_notif_citas> GetT_notif_citas()
        {
            return db.T_notif_citas;
        }

        // GET: api/T_notif_citas/5
        [ResponseType(typeof(T_notif_citas))]
        public async Task<IHttpActionResult> GetT_notif_citas(int id)
        {
            T_notif_citas t_notif_citas = await db.T_notif_citas.FindAsync(id);
            if (t_notif_citas == null)
            {
                return NotFound();
            }

            return Ok(t_notif_citas);
        }

        // PUT: api/T_notif_citas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_notif_citas(int id, T_notif_citas t_notif_citas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_notif_citas.Id_Notif_Cita)
            {
                return BadRequest();
            }

            db.Entry(t_notif_citas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_notif_citasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_notif_citas);
        }

        // POST: api/T_notif_citas
        [ResponseType(typeof(T_notif_citas))]
        public async Task<IHttpActionResult> PostT_notif_citas(T_notif_citas t_notif_citas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_notif_citas.Add(t_notif_citas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_notif_citas.Id_Notif_Cita }, t_notif_citas);
        }

        // DELETE: api/T_notif_citas/5
        [ResponseType(typeof(T_notif_citas))]
        public async Task<IHttpActionResult> DeleteT_notif_citas(int id)
        {
            T_notif_citas t_notif_citas = await db.T_notif_citas.FindAsync(id);
            if (t_notif_citas == null)
            {
                return NotFound();
            }

            db.T_notif_citas.Remove(t_notif_citas);
            await db.SaveChangesAsync();

            return Ok(t_notif_citas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_notif_citasExists(int id)
        {
            return db.T_notif_citas.Count(e => e.Id_Notif_Cita == id) > 0;
        }
    }
}