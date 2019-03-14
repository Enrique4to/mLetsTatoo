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
    public class TpropietariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tpropietarios
        public IQueryable<Tpropietarios> GetTpropietarios()
        {
            return db.Tpropietarios;
        }

        // GET: api/Tpropietarios/5
        [ResponseType(typeof(Tpropietarios))]
        public async Task<IHttpActionResult> GetTpropietarios(int id)
        {
            Tpropietarios tpropietarios = await db.Tpropietarios.FindAsync(id);
            if (tpropietarios == null)
            {
                return NotFound();
            }

            return Ok(tpropietarios);
        }

        // PUT: api/Tpropietarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTpropietarios(int id, Tpropietarios tpropietarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tpropietarios.Id_Propietario)
            {
                return BadRequest();
            }

            db.Entry(tpropietarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TpropietariosExists(id))
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

        // POST: api/Tpropietarios
        [ResponseType(typeof(Tpropietarios))]
        public async Task<IHttpActionResult> PostTpropietarios(Tpropietarios tpropietarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tpropietarios.Add(tpropietarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tpropietarios.Id_Propietario }, tpropietarios);
        }

        // DELETE: api/Tpropietarios/5
        [ResponseType(typeof(Tpropietarios))]
        public async Task<IHttpActionResult> DeleteTpropietarios(int id)
        {
            Tpropietarios tpropietarios = await db.Tpropietarios.FindAsync(id);
            if (tpropietarios == null)
            {
                return NotFound();
            }

            db.Tpropietarios.Remove(tpropietarios);
            await db.SaveChangesAsync();

            return Ok(tpropietarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TpropietariosExists(int id)
        {
            return db.Tpropietarios.Count(e => e.Id_Propietario == id) > 0;
        }
    }
}