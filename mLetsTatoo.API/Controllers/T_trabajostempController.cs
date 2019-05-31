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
    public class T_trabajostempController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_trabajostemp
        public IQueryable<T_trabajostemp> GetT_trabajostemp()
        {
            return db.T_trabajostemp;
        }

        // GET: api/T_trabajostemp/5
        [ResponseType(typeof(T_trabajostemp))]
        public async Task<IHttpActionResult> GetT_trabajostemp(int id)
        {
            T_trabajostemp t_trabajostemp = await db.T_trabajostemp.FindAsync(id);
            if (t_trabajostemp == null)
            {
                return NotFound();
            }

            return Ok(t_trabajostemp);
        }

        // PUT: api/T_trabajostemp/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_trabajostemp(int id, T_trabajostemp t_trabajostemp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_trabajostemp.Id_Trabajotemp)
            {
                return BadRequest();
            }

            db.Entry(t_trabajostemp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_trabajostempExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_trabajostemp);
        }

        // POST: api/T_trabajostemp
        [ResponseType(typeof(T_trabajostemp))]
        public async Task<IHttpActionResult> PostT_trabajostemp(T_trabajostemp t_trabajostemp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_trabajostemp.Add(t_trabajostemp);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_trabajostemp.Id_Trabajotemp }, t_trabajostemp);
        }

        // DELETE: api/T_trabajostemp/5
        [ResponseType(typeof(T_trabajostemp))]
        public async Task<IHttpActionResult> DeleteT_trabajostemp(int id)
        {
            T_trabajostemp t_trabajostemp = await db.T_trabajostemp.FindAsync(id);
            if (t_trabajostemp == null)
            {
                return NotFound();
            }

            db.T_trabajostemp.Remove(t_trabajostemp);
            await db.SaveChangesAsync();

            return Ok(t_trabajostemp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_trabajostempExists(int id)
        {
            return db.T_trabajostemp.Count(e => e.Id_Trabajotemp == id) > 0;
        }
    }
}