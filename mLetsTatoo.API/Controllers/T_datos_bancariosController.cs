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
using mLetsTatoo.Models;
using mLetsTatoo.Domain.Modules;

namespace mLetsTatoo.API.Controllers
{
    public class T_datos_bancariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_datos_bancarios
        public IQueryable<T_datos_bancarios> GetT_datos_bancarios()
        {
            return db.T_datos_bancarios;
        }

        // GET: api/T_datos_bancarios/5
        [ResponseType(typeof(T_datos_bancarios))]
        public async Task<IHttpActionResult> GetT_datos_bancarios(int id)
        {
            T_datos_bancarios t_datos_bancarios = await db.T_datos_bancarios.FindAsync(id);
            if (t_datos_bancarios == null)
            {
                return NotFound();
            }

            return Ok(t_datos_bancarios);
        }

        // PUT: api/T_datos_bancarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_datos_bancarios(int id, T_datos_bancarios t_datos_bancarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_datos_bancarios.Id_DatoBancario)
            {
                return BadRequest();
            }

            db.Entry(t_datos_bancarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_datos_bancariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_datos_bancarios);
        }

        // POST: api/T_datos_bancarios
        [ResponseType(typeof(T_datos_bancarios))]
        public async Task<IHttpActionResult> PostT_datos_bancarios(T_datos_bancarios t_datos_bancarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_datos_bancarios.Add(t_datos_bancarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_datos_bancarios.Id_DatoBancario }, t_datos_bancarios);
        }

        // DELETE: api/T_datos_bancarios/5
        [ResponseType(typeof(T_datos_bancarios))]
        public async Task<IHttpActionResult> DeleteT_datos_bancarios(int id)
        {
            T_datos_bancarios t_datos_bancarios = await db.T_datos_bancarios.FindAsync(id);
            if (t_datos_bancarios == null)
            {
                return NotFound();
            }

            db.T_datos_bancarios.Remove(t_datos_bancarios);
            await db.SaveChangesAsync();

            return Ok(t_datos_bancarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_datos_bancariosExists(int id)
        {
            return db.T_datos_bancarios.Count(e => e.Id_DatoBancario == id) > 0;
        }
    }
}