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
    public class T_tecnicohorariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_tecnicohorarios
        public IQueryable<T_tecnicohorarios> GetT_tecnicohorarios()
        {
            return db.T_tecnicohorarios;
        }

        // GET: api/T_tecnicohorarios/5
        [ResponseType(typeof(T_tecnicohorarios))]
        public async Task<IHttpActionResult> GetT_tecnicohorarios(int id)
        {
            T_tecnicohorarios t_tecnicohorarios = await db.T_tecnicohorarios.FindAsync(id);
            if (t_tecnicohorarios == null)
            {
                return NotFound();
            }

            return Ok(t_tecnicohorarios);
        }

        // PUT: api/T_tecnicohorarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_tecnicohorarios(int id, T_tecnicohorarios t_tecnicohorarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_tecnicohorarios.Id_Horario)
            {
                return BadRequest();
            }

            db.Entry(t_tecnicohorarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_tecnicohorariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_tecnicohorarios);
        }

        // POST: api/T_tecnicohorarios
        [ResponseType(typeof(T_tecnicohorarios))]
        public async Task<IHttpActionResult> PostT_tecnicohorarios(T_tecnicohorarios t_tecnicohorarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_tecnicohorarios.Add(t_tecnicohorarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_tecnicohorarios.Id_Horario }, t_tecnicohorarios);
        }

        // DELETE: api/T_tecnicohorarios/5
        [ResponseType(typeof(T_tecnicohorarios))]
        public async Task<IHttpActionResult> DeleteT_tecnicohorarios(int id)
        {
            T_tecnicohorarios t_tecnicohorarios = await db.T_tecnicohorarios.FindAsync(id);
            if (t_tecnicohorarios == null)
            {
                return NotFound();
            }

            db.T_tecnicohorarios.Remove(t_tecnicohorarios);
            await db.SaveChangesAsync();

            return Ok(t_tecnicohorarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_tecnicohorariosExists(int id)
        {
            return db.T_tecnicohorarios.Count(e => e.Id_Horario == id) > 0;
        }
    }
}