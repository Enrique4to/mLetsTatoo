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
    public class Ttrabajo_notaController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Ttrabajo_nota
        public IQueryable<Ttrabajo_nota> GetTtrabajo_nota()
        {
            return db.Ttrabajo_nota;
        }

        // GET: api/Ttrabajo_nota/5
        [ResponseType(typeof(Ttrabajo_nota))]
        public async Task<IHttpActionResult> GetTtrabajo_nota(int id)
        {
            Ttrabajo_nota ttrabajo_nota = await db.Ttrabajo_nota.FindAsync(id);
            if (ttrabajo_nota == null)
            {
                return NotFound();
            }

            return Ok(ttrabajo_nota);
        }

        // PUT: api/Ttrabajo_nota/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTtrabajo_nota(int id, Ttrabajo_nota ttrabajo_nota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ttrabajo_nota.Id_Nota)
            {
                return BadRequest();
            }

            db.Entry(ttrabajo_nota).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Ttrabajo_notaExists(id))
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

        // POST: api/Ttrabajo_nota
        [ResponseType(typeof(Ttrabajo_nota))]
        public async Task<IHttpActionResult> PostTtrabajo_nota(Ttrabajo_nota ttrabajo_nota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ttrabajo_nota.Add(ttrabajo_nota);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ttrabajo_nota.Id_Nota }, ttrabajo_nota);
        }

        // DELETE: api/Ttrabajo_nota/5
        [ResponseType(typeof(Ttrabajo_nota))]
        public async Task<IHttpActionResult> DeleteTtrabajo_nota(int id)
        {
            Ttrabajo_nota ttrabajo_nota = await db.Ttrabajo_nota.FindAsync(id);
            if (ttrabajo_nota == null)
            {
                return NotFound();
            }

            db.Ttrabajo_nota.Remove(ttrabajo_nota);
            await db.SaveChangesAsync();

            return Ok(ttrabajo_nota);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Ttrabajo_notaExists(int id)
        {
            return db.Ttrabajo_nota.Count(e => e.Id_Nota == id) > 0;
        }
    }
}