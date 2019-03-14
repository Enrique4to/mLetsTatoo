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
    public class Tlocal_historiaController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Tlocal_historia
        public IQueryable<Tlocal_historia> GetTlocal_historia()
        {
            return db.Tlocal_historia;
        }

        // GET: api/Tlocal_historia/5
        [ResponseType(typeof(Tlocal_historia))]
        public async Task<IHttpActionResult> GetTlocal_historia(int id)
        {
            Tlocal_historia tlocal_historia = await db.Tlocal_historia.FindAsync(id);
            if (tlocal_historia == null)
            {
                return NotFound();
            }

            return Ok(tlocal_historia);
        }

        // PUT: api/Tlocal_historia/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTlocal_historia(int id, Tlocal_historia tlocal_historia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tlocal_historia.Id_Historia)
            {
                return BadRequest();
            }

            db.Entry(tlocal_historia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tlocal_historiaExists(id))
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

        // POST: api/Tlocal_historia
        [ResponseType(typeof(Tlocal_historia))]
        public async Task<IHttpActionResult> PostTlocal_historia(Tlocal_historia tlocal_historia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tlocal_historia.Add(tlocal_historia);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tlocal_historia.Id_Historia }, tlocal_historia);
        }

        // DELETE: api/Tlocal_historia/5
        [ResponseType(typeof(Tlocal_historia))]
        public async Task<IHttpActionResult> DeleteTlocal_historia(int id)
        {
            Tlocal_historia tlocal_historia = await db.Tlocal_historia.FindAsync(id);
            if (tlocal_historia == null)
            {
                return NotFound();
            }

            db.Tlocal_historia.Remove(tlocal_historia);
            await db.SaveChangesAsync();

            return Ok(tlocal_historia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tlocal_historiaExists(int id)
        {
            return db.Tlocal_historia.Count(e => e.Id_Historia == id) > 0;
        }
    }
}