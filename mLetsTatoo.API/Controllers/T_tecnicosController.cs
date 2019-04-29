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
    public class T_tecnicosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_tecnicos
        public IQueryable<T_tecnicos> GetT_tecnicos()
        {
            return db.T_tecnicos;
        }

        // GET: api/T_tecnicos/5
        [ResponseType(typeof(T_tecnicos))]
        public async Task<IHttpActionResult> GetT_tecnicos(int id)
        {
            T_tecnicos t_tecnicos = await db.T_tecnicos.FindAsync(id);
            if (t_tecnicos == null)
            {
                return NotFound();
            }

            return Ok(t_tecnicos);
        }

        // PUT: api/T_tecnicos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_tecnicos(int id, T_tecnicos t_tecnicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_tecnicos.Id_Tecnico)
            {
                return BadRequest();
            }

            db.Entry(t_tecnicos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_tecnicosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_tecnicos);
        }

        // POST: api/T_tecnicos
        [ResponseType(typeof(T_tecnicos))]
        public async Task<IHttpActionResult> PostT_tecnicos(T_tecnicos t_tecnicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_tecnicos.Add(t_tecnicos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_tecnicos.Id_Tecnico }, t_tecnicos);
        }

        // DELETE: api/T_tecnicos/5
        [ResponseType(typeof(T_tecnicos))]
        public async Task<IHttpActionResult> DeleteT_tecnicos(int id)
        {
            T_tecnicos t_tecnicos = await db.T_tecnicos.FindAsync(id);
            if (t_tecnicos == null)
            {
                return NotFound();
            }

            db.T_tecnicos.Remove(t_tecnicos);
            await db.SaveChangesAsync();

            return Ok(t_tecnicos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_tecnicosExists(int id)
        {
            return db.T_tecnicos.Count(e => e.Id_Tecnico == id) > 0;
        }
    }
}