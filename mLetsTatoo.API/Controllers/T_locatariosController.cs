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
    public class T_locatariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_locatarios
        public IQueryable<T_locatarios> GetT_locatarios()
        {
            return db.T_locatarios;
        }

        // GET: api/T_locatarios/5
        [ResponseType(typeof(T_locatarios))]
        public async Task<IHttpActionResult> GetT_locatarios(int id)
        {
            T_locatarios t_locatarios = await db.T_locatarios.FindAsync(id);
            if (t_locatarios == null)
            {
                return NotFound();
            }

            return Ok(t_locatarios);
        }

        // PUT: api/T_locatarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_locatarios(int id, T_locatarios t_locatarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_locatarios.Id_Locatario)
            {
                return BadRequest();
            }

            db.Entry(t_locatarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_locatariosExists(id))
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

        // POST: api/T_locatarios
        [ResponseType(typeof(T_locatarios))]
        public async Task<IHttpActionResult> PostT_locatarios(T_locatarios t_locatarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_locatarios.Add(t_locatarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_locatarios.Id_Locatario }, t_locatarios);
        }

        // DELETE: api/T_locatarios/5
        [ResponseType(typeof(T_locatarios))]
        public async Task<IHttpActionResult> DeleteT_locatarios(int id)
        {
            T_locatarios t_locatarios = await db.T_locatarios.FindAsync(id);
            if (t_locatarios == null)
            {
                return NotFound();
            }

            db.T_locatarios.Remove(t_locatarios);
            await db.SaveChangesAsync();

            return Ok(t_locatarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_locatariosExists(int id)
        {
            return db.T_locatarios.Count(e => e.Id_Locatario == id) > 0;
        }
    }
}