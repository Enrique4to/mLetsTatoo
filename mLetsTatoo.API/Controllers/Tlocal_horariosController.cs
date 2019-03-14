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
    public class Tlocal_horariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tlocal_horarios
        public IQueryable<Tlocal_horarios> GetTlocal_horarios()
        {
            return db.Tlocal_horarios;
        }

        // GET: api/Tlocal_horarios/5
        [ResponseType(typeof(Tlocal_horarios))]
        public async Task<IHttpActionResult> GetTlocal_horarios(int id)
        {
            Tlocal_horarios tlocal_horarios = await db.Tlocal_horarios.FindAsync(id);
            if (tlocal_horarios == null)
            {
                return NotFound();
            }

            return Ok(tlocal_horarios);
        }

        // PUT: api/Tlocal_horarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTlocal_horarios(int id, Tlocal_horarios tlocal_horarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tlocal_horarios.Id_Horario)
            {
                return BadRequest();
            }

            db.Entry(tlocal_horarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tlocal_horariosExists(id))
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

        // POST: api/Tlocal_horarios
        [ResponseType(typeof(Tlocal_horarios))]
        public async Task<IHttpActionResult> PostTlocal_horarios(Tlocal_horarios tlocal_horarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tlocal_horarios.Add(tlocal_horarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tlocal_horarios.Id_Horario }, tlocal_horarios);
        }

        // DELETE: api/Tlocal_horarios/5
        [ResponseType(typeof(Tlocal_horarios))]
        public async Task<IHttpActionResult> DeleteTlocal_horarios(int id)
        {
            Tlocal_horarios tlocal_horarios = await db.Tlocal_horarios.FindAsync(id);
            if (tlocal_horarios == null)
            {
                return NotFound();
            }

            db.Tlocal_horarios.Remove(tlocal_horarios);
            await db.SaveChangesAsync();

            return Ok(tlocal_horarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tlocal_horariosExists(int id)
        {
            return db.Tlocal_horarios.Count(e => e.Id_Horario == id) > 0;
        }
    }
}