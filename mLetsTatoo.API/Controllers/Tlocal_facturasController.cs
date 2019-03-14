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
    public class Tlocal_facturasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tlocal_facturas
        public IQueryable<Tlocal_facturas> GetTlocal_facturas()
        {
            return db.Tlocal_facturas;
        }

        // GET: api/Tlocal_facturas/5
        [ResponseType(typeof(Tlocal_facturas))]
        public async Task<IHttpActionResult> GetTlocal_facturas(int id)
        {
            Tlocal_facturas tlocal_facturas = await db.Tlocal_facturas.FindAsync(id);
            if (tlocal_facturas == null)
            {
                return NotFound();
            }

            return Ok(tlocal_facturas);
        }

        // PUT: api/Tlocal_facturas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTlocal_facturas(int id, Tlocal_facturas tlocal_facturas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tlocal_facturas.Id_Lfactura)
            {
                return BadRequest();
            }

            db.Entry(tlocal_facturas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tlocal_facturasExists(id))
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

        // POST: api/Tlocal_facturas
        [ResponseType(typeof(Tlocal_facturas))]
        public async Task<IHttpActionResult> PostTlocal_facturas(Tlocal_facturas tlocal_facturas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tlocal_facturas.Add(tlocal_facturas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tlocal_facturas.Id_Lfactura }, tlocal_facturas);
        }

        // DELETE: api/Tlocal_facturas/5
        [ResponseType(typeof(Tlocal_facturas))]
        public async Task<IHttpActionResult> DeleteTlocal_facturas(int id)
        {
            Tlocal_facturas tlocal_facturas = await db.Tlocal_facturas.FindAsync(id);
            if (tlocal_facturas == null)
            {
                return NotFound();
            }

            db.Tlocal_facturas.Remove(tlocal_facturas);
            await db.SaveChangesAsync();

            return Ok(tlocal_facturas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tlocal_facturasExists(int id)
        {
            return db.Tlocal_facturas.Count(e => e.Id_Lfactura == id) > 0;
        }
    }
}