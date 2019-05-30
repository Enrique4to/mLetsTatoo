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
using mLetsTatoo.Models;
using mLetsTatoo.Domain.Modules;

namespace mLetsTatoo.API.Controllers
{
    public class T_retirosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_retiros
        public IQueryable<T_retiros> GetT_retiros()
        {
            return db.T_retiros;
        }

        // GET: api/T_retiros/5
        [ResponseType(typeof(T_retiros))]
        public async Task<IHttpActionResult> GetT_retiros(int id)
        {
            T_retiros t_retiros = await db.T_retiros.FindAsync(id);
            if (t_retiros == null)
            {
                return NotFound();
            }

            return Ok(t_retiros);
        }

        // PUT: api/T_retiros/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_retiros(int id, T_retiros t_retiros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_retiros.Id_Retiros)
            {
                return BadRequest();
            }

            db.Entry(t_retiros).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_retirosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_retiros);
        }

        // POST: api/T_retiros
        [ResponseType(typeof(T_retiros))]
        public async Task<IHttpActionResult> PostT_retiros(T_retiros t_retiros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_retiros.Add(t_retiros);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_retiros.Id_Retiros }, t_retiros);
        }

        // DELETE: api/T_retiros/5
        [ResponseType(typeof(T_retiros))]
        public async Task<IHttpActionResult> DeleteT_retiros(int id)
        {
            T_retiros t_retiros = await db.T_retiros.FindAsync(id);
            if (t_retiros == null)
            {
                return NotFound();
            }

            db.T_retiros.Remove(t_retiros);
            await db.SaveChangesAsync();

            return Ok(t_retiros);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_retirosExists(int id)
        {
            return db.T_retiros.Count(e => e.Id_Retiros == id) > 0;
        }
    }
}