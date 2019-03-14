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
    public class TlocatariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tlocatarios
        public IQueryable<Tlocatarios> GetTlocatarios()
        {
            return db.Tlocatarios;
        }

        // GET: api/Tlocatarios/5
        [ResponseType(typeof(Tlocatarios))]
        public async Task<IHttpActionResult> GetTlocatarios(int id)
        {
            Tlocatarios tlocatarios = await db.Tlocatarios.FindAsync(id);
            if (tlocatarios == null)
            {
                return NotFound();
            }

            return Ok(tlocatarios);
        }

        // PUT: api/Tlocatarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTlocatarios(int id, Tlocatarios tlocatarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tlocatarios.Id_Locatario)
            {
                return BadRequest();
            }

            db.Entry(tlocatarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TlocatariosExists(id))
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

        // POST: api/Tlocatarios
        [ResponseType(typeof(Tlocatarios))]
        public async Task<IHttpActionResult> PostTlocatarios(Tlocatarios tlocatarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tlocatarios.Add(tlocatarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tlocatarios.Id_Locatario }, tlocatarios);
        }

        // DELETE: api/Tlocatarios/5
        [ResponseType(typeof(Tlocatarios))]
        public async Task<IHttpActionResult> DeleteTlocatarios(int id)
        {
            Tlocatarios tlocatarios = await db.Tlocatarios.FindAsync(id);
            if (tlocatarios == null)
            {
                return NotFound();
            }

            db.Tlocatarios.Remove(tlocatarios);
            await db.SaveChangesAsync();

            return Ok(tlocatarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TlocatariosExists(int id)
        {
            return db.Tlocatarios.Count(e => e.Id_Locatario == id) > 0;
        }
    }
}