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
    public class TusuariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tusuarios
        public IQueryable<Tusuarios> GetTusuarios()
        {
            return db.Tusuarios;
        }

        // GET: api/Tusuarios/5
        [ResponseType(typeof(Tusuarios))]
        public async Task<IHttpActionResult> GetTusuarios(int id)
        {
            Tusuarios tusuarios = await db.Tusuarios.FindAsync(id);
            if (tusuarios == null)
            {
                return NotFound();
            }

            return Ok(tusuarios);
        }

        // PUT: api/Tusuarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTusuarios(int id, Tusuarios tusuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tusuarios.Id_usuario)
            {
                return BadRequest();
            }

            db.Entry(tusuarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TusuariosExists(id))
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

        // POST: api/Tusuarios
        [ResponseType(typeof(Tusuarios))]
        public async Task<IHttpActionResult> PostTusuarios(Tusuarios tusuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tusuarios.Add(tusuarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tusuarios.Id_usuario }, tusuarios);
        }

        // DELETE: api/Tusuarios/5
        [ResponseType(typeof(Tusuarios))]
        public async Task<IHttpActionResult> DeleteTusuarios(int id)
        {
            Tusuarios tusuarios = await db.Tusuarios.FindAsync(id);
            if (tusuarios == null)
            {
                return NotFound();
            }

            db.Tusuarios.Remove(tusuarios);
            await db.SaveChangesAsync();

            return Ok(tusuarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TusuariosExists(int id)
        {
            return db.Tusuarios.Count(e => e.Id_usuario == id) > 0;
        }
    }
}