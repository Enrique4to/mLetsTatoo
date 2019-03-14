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
    public class TclientesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tclientes
        public IQueryable<Tclientes> GetTclientes()
        {
            return db.Tclientes;
        }

        // GET: api/Tclientes/5
        [ResponseType(typeof(Tclientes))]
        public async Task<IHttpActionResult> GetTclientes(int id)
        {
            Tclientes tclientes = await db.Tclientes.FindAsync(id);
            if (tclientes == null)
            {
                return NotFound();
            }

            return Ok(tclientes);
        }

        // PUT: api/Tclientes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTclientes(int id, Tclientes tclientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tclientes.Id_Cliente)
            {
                return BadRequest();
            }

            db.Entry(tclientes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TclientesExists(id))
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

        // POST: api/Tclientes
        [ResponseType(typeof(Tclientes))]
        public async Task<IHttpActionResult> PostTclientes(Tclientes tclientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tclientes.Add(tclientes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tclientes.Id_Cliente }, tclientes);
        }

        // DELETE: api/Tclientes/5
        [ResponseType(typeof(Tclientes))]
        public async Task<IHttpActionResult> DeleteTclientes(int id)
        {
            Tclientes tclientes = await db.Tclientes.FindAsync(id);
            if (tclientes == null)
            {
                return NotFound();
            }

            db.Tclientes.Remove(tclientes);
            await db.SaveChangesAsync();

            return Ok(tclientes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TclientesExists(int id)
        {
            return db.Tclientes.Count(e => e.Id_Cliente == id) > 0;
        }
    }
}