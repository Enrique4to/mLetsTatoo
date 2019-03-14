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
    public class TestadoController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Testado
        public IQueryable<Testado> GetTestadoes()
        {
            return db.Testadoes;
        }

        // GET: api/Testado/5
        [ResponseType(typeof(Testado))]
        public async Task<IHttpActionResult> GetTestado(int id)
        {
            Testado testado = await db.Testadoes.FindAsync(id);
            if (testado == null)
            {
                return NotFound();
            }

            return Ok(testado);
        }

        // PUT: api/Testado/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTestado(int id, Testado testado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != testado.Id)
            {
                return BadRequest();
            }

            db.Entry(testado).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestadoExists(id))
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

        // POST: api/Testado
        [ResponseType(typeof(Testado))]
        public async Task<IHttpActionResult> PostTestado(Testado testado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Testadoes.Add(testado);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = testado.Id }, testado);
        }

        // DELETE: api/Testado/5
        [ResponseType(typeof(Testado))]
        public async Task<IHttpActionResult> DeleteTestado(int id)
        {
            Testado testado = await db.Testadoes.FindAsync(id);
            if (testado == null)
            {
                return NotFound();
            }

            db.Testadoes.Remove(testado);
            await db.SaveChangesAsync();

            return Ok(testado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestadoExists(int id)
        {
            return db.Testadoes.Count(e => e.Id == id) > 0;
        }
    }
}