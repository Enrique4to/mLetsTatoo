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
    public class T_localhorariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_localhorarios
        public IQueryable<T_localhorarios> GetT_localhorarios()
        {
            return db.T_localhorarios;
        }

        // GET: api/T_localhorarios/5
        [ResponseType(typeof(T_localhorarios))]
        public async Task<IHttpActionResult> GetT_localhorarios(int id)
        {
            T_localhorarios t_localhorarios = await db.T_localhorarios.FindAsync(id);
            if (t_localhorarios == null)
            {
                return NotFound();
            }

            return Ok(t_localhorarios);
        }

        // PUT: api/T_localhorarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_localhorarios(int id, T_localhorarios t_localhorarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_localhorarios.Id_Horario)
            {
                return BadRequest();
            }

            db.Entry(t_localhorarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_localhorariosExists(id))
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

        // POST: api/T_localhorarios
        [ResponseType(typeof(T_localhorarios))]
        public async Task<IHttpActionResult> PostT_localhorarios(T_localhorarios t_localhorarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_localhorarios.Add(t_localhorarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_localhorarios.Id_Horario }, t_localhorarios);
        }

        // DELETE: api/T_localhorarios/5
        [ResponseType(typeof(T_localhorarios))]
        public async Task<IHttpActionResult> DeleteT_localhorarios(int id)
        {
            T_localhorarios t_localhorarios = await db.T_localhorarios.FindAsync(id);
            if (t_localhorarios == null)
            {
                return NotFound();
            }

            db.T_localhorarios.Remove(t_localhorarios);
            await db.SaveChangesAsync();

            return Ok(t_localhorarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_localhorariosExists(int id)
        {
            return db.T_localhorarios.Count(e => e.Id_Horario == id) > 0;
        }
    }
}