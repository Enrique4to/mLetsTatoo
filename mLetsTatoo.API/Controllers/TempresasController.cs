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
    public class TempresasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tempresas
        public IQueryable<Tempresas> GetTempresas()
        {
            return db.Tempresas;
        }

        // GET: api/Tempresas/5
        [ResponseType(typeof(Tempresas))]
        public async Task<IHttpActionResult> GetTempresas(int id)
        {
            Tempresas tempresas = await db.Tempresas.FindAsync(id);
            if (tempresas == null)
            {
                return NotFound();
            }

            return Ok(tempresas);
        }

        // PUT: api/Tempresas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTempresas(int id, Tempresas tempresas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tempresas.Id_Empresa)
            {
                return BadRequest();
            }

            db.Entry(tempresas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempresasExists(id))
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

        // POST: api/Tempresas
        [ResponseType(typeof(Tempresas))]
        public async Task<IHttpActionResult> PostTempresas(Tempresas tempresas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tempresas.Add(tempresas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tempresas.Id_Empresa }, tempresas);
        }

        // DELETE: api/Tempresas/5
        [ResponseType(typeof(Tempresas))]
        public async Task<IHttpActionResult> DeleteTempresas(int id)
        {
            Tempresas tempresas = await db.Tempresas.FindAsync(id);
            if (tempresas == null)
            {
                return NotFound();
            }

            db.Tempresas.Remove(tempresas);
            await db.SaveChangesAsync();

            return Ok(tempresas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TempresasExists(int id)
        {
            return db.Tempresas.Count(e => e.Id_Empresa == id) > 0;
        }
    }
}