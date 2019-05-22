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
    public class T_balanceempresaController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_balanceempresa
        public IQueryable<T_balanceempresa> GetT_balanceempresa()
        {
            return db.T_balanceempresa;
        }

        // GET: api/T_balanceempresa/5
        [ResponseType(typeof(T_balanceempresa))]
        public async Task<IHttpActionResult> GetT_balanceempresa(int id)
        {
            T_balanceempresa t_balanceempresa = await db.T_balanceempresa.FindAsync(id);
            if (t_balanceempresa == null)
            {
                return NotFound();
            }

            return Ok(t_balanceempresa);
        }

        // PUT: api/T_balanceempresa/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_balanceempresa(int id, T_balanceempresa t_balanceempresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_balanceempresa.Id_Balanceempresa)
            {
                return BadRequest();
            }

            db.Entry(t_balanceempresa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_balanceempresaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_balanceempresa);
        }

        // POST: api/T_balanceempresa
        [ResponseType(typeof(T_balanceempresa))]
        public async Task<IHttpActionResult> PostT_balanceempresa(T_balanceempresa t_balanceempresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_balanceempresa.Add(t_balanceempresa);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_balanceempresa.Id_Balanceempresa }, t_balanceempresa);
        }

        // DELETE: api/T_balanceempresa/5
        [ResponseType(typeof(T_balanceempresa))]
        public async Task<IHttpActionResult> DeleteT_balanceempresa(int id)
        {
            T_balanceempresa t_balanceempresa = await db.T_balanceempresa.FindAsync(id);
            if (t_balanceempresa == null)
            {
                return NotFound();
            }

            db.T_balanceempresa.Remove(t_balanceempresa);
            await db.SaveChangesAsync();

            return Ok(t_balanceempresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_balanceempresaExists(int id)
        {
            return db.T_balanceempresa.Count(e => e.Id_Balanceempresa == id) > 0;
        }
    }
}