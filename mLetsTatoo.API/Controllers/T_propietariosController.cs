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
    public class T_propietariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_propietarios
        public IQueryable<T_propietarios> GetT_propietarios()
        {
            return db.T_propietarios;
        }

        // GET: api/T_propietarios/5
        [ResponseType(typeof(T_propietarios))]
        public async Task<IHttpActionResult> GetT_propietarios(int id)
        {
            T_propietarios t_propietarios = await db.T_propietarios.FindAsync(id);
            if (t_propietarios == null)
            {
                return NotFound();
            }

            return Ok(t_propietarios);
        }

        // PUT: api/T_propietarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_propietarios(int id, T_propietarios t_propietarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_propietarios.Id_Propietario)
            {
                return BadRequest();
            }

            db.Entry(t_propietarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_propietariosExists(id))
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

        // POST: api/T_propietarios
        [ResponseType(typeof(T_propietarios))]
        public async Task<IHttpActionResult> PostT_propietarios(T_propietarios t_propietarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_propietarios.Add(t_propietarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_propietarios.Id_Propietario }, t_propietarios);
        }

        // DELETE: api/T_propietarios/5
        [ResponseType(typeof(T_propietarios))]
        public async Task<IHttpActionResult> DeleteT_propietarios(int id)
        {
            T_propietarios t_propietarios = await db.T_propietarios.FindAsync(id);
            if (t_propietarios == null)
            {
                return NotFound();
            }

            db.T_propietarios.Remove(t_propietarios);
            await db.SaveChangesAsync();

            return Ok(t_propietarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_propietariosExists(int id)
        {
            return db.T_propietarios.Count(e => e.Id_Propietario == id) > 0;
        }
    }
}