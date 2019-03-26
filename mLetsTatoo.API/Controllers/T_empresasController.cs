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
    public class T_empresasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_empresas
        public IQueryable<T_empresas> GetT_empresas()
        {
            return db.T_empresas;
        }

        // GET: api/T_empresas/5
        [ResponseType(typeof(T_empresas))]
        public async Task<IHttpActionResult> GetT_empresas(int id)
        {
            T_empresas t_empresas = await db.T_empresas.FindAsync(id);
            if (t_empresas == null)
            {
                return NotFound();
            }

            return Ok(t_empresas);
        }

        // PUT: api/T_empresas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_empresas(int id, T_empresas t_empresas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_empresas.Id_Empresa)
            {
                return BadRequest();
            }

            db.Entry(t_empresas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_empresasExists(id))
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

        // POST: api/T_empresas
        [ResponseType(typeof(T_empresas))]
        public async Task<IHttpActionResult> PostT_empresas(T_empresas t_empresas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_empresas.Add(t_empresas);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_empresas.Id_Empresa }, t_empresas);
        }

        // DELETE: api/T_empresas/5
        [ResponseType(typeof(T_empresas))]
        public async Task<IHttpActionResult> DeleteT_empresas(int id)
        {
            T_empresas t_empresas = await db.T_empresas.FindAsync(id);
            if (t_empresas == null)
            {
                return NotFound();
            }

            db.T_empresas.Remove(t_empresas);
            await db.SaveChangesAsync();

            return Ok(t_empresas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_empresasExists(int id)
        {
            return db.T_empresas.Count(e => e.Id_Empresa == id) > 0;
        }
    }
}