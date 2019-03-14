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
    public class TtecnicosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Ttecnicos
        public IQueryable<Ttecnicos> GetTtecnicos()
        {
            return db.Ttecnicos;
        }

        // GET: api/Ttecnicos/5
        [ResponseType(typeof(Ttecnicos))]
        public async Task<IHttpActionResult> GetTtecnicos(int id)
        {
            Ttecnicos ttecnicos = await db.Ttecnicos.FindAsync(id);
            if (ttecnicos == null)
            {
                return NotFound();
            }

            return Ok(ttecnicos);
        }

        // PUT: api/Ttecnicos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTtecnicos(int id, Ttecnicos ttecnicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ttecnicos.Id_Tecnico)
            {
                return BadRequest();
            }

            db.Entry(ttecnicos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TtecnicosExists(id))
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

        // POST: api/Ttecnicos
        [ResponseType(typeof(Ttecnicos))]
        public async Task<IHttpActionResult> PostTtecnicos(Ttecnicos ttecnicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ttecnicos.Add(ttecnicos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ttecnicos.Id_Tecnico }, ttecnicos);
        }

        // DELETE: api/Ttecnicos/5
        [ResponseType(typeof(Ttecnicos))]
        public async Task<IHttpActionResult> DeleteTtecnicos(int id)
        {
            Ttecnicos ttecnicos = await db.Ttecnicos.FindAsync(id);
            if (ttecnicos == null)
            {
                return NotFound();
            }

            db.Ttecnicos.Remove(ttecnicos);
            await db.SaveChangesAsync();

            return Ok(ttecnicos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TtecnicosExists(int id)
        {
            return db.Ttecnicos.Count(e => e.Id_Tecnico == id) > 0;
        }
    }
}