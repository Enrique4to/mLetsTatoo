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
    public class T_localfacturasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_localfacturas
        public IQueryable<T_localfacturas> GetT_localfacturas()
        {
            return db.T_localfacturas;
        }

        // GET: api/T_localfacturas/5
        [ResponseType(typeof(T_localfacturas))]
        public async Task<IHttpActionResult> GetT_localfacturas(int id)
        {
            T_localfacturas t_localfacturas = await db.T_localfacturas.FindAsync(id);
            if (t_localfacturas == null)
            {
                return NotFound();
            }

            return Ok(t_localfacturas);
        }

        // PUT: api/T_localfacturas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_localfacturas(int id, T_localfacturas t_localfacturas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_localfacturas.Id_Lfactura)
            {
                return BadRequest();
            }

            db.Entry(t_localfacturas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_localfacturasExists(id))
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

        // POST: api/T_localfacturas
        [ResponseType(typeof(T_localfacturas))]
        public async Task<IHttpActionResult> PostT_localfacturas(T_localfacturas t_localfacturas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_localfacturas.Add(t_localfacturas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_localfacturas.Id_Lfactura }, t_localfacturas);
        }

        // DELETE: api/T_localfacturas/5
        [ResponseType(typeof(T_localfacturas))]
        public async Task<IHttpActionResult> DeleteT_localfacturas(int id)
        {
            T_localfacturas t_localfacturas = await db.T_localfacturas.FindAsync(id);
            if (t_localfacturas == null)
            {
                return NotFound();
            }

            db.T_localfacturas.Remove(t_localfacturas);
            await db.SaveChangesAsync();

            return Ok(t_localfacturas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_localfacturasExists(int id)
        {
            return db.T_localfacturas.Count(e => e.Id_Lfactura == id) > 0;
        }
    }
}