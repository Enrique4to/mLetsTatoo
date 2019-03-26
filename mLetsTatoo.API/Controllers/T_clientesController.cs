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
    public class T_clientesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_clientes
        public IQueryable<T_clientes> GetT_clientes()
        {
            return db.T_clientes;
        }

        // GET: api/T_clientes/5
        [ResponseType(typeof(T_clientes))]
        public async Task<IHttpActionResult> GetT_clientes(int id)
        {
            T_clientes t_clientes = await db.T_clientes.FindAsync(id);
            if (t_clientes == null)
            {
                return NotFound();
            }

            return Ok(t_clientes);
        }

        // PUT: api/T_clientes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_clientes(int id, T_clientes t_clientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_clientes.Id_Cliente)
            {
                return BadRequest();
            }

            db.Entry(t_clientes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_clientesExists(id))
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

        // POST: api/T_clientes
        [ResponseType(typeof(T_clientes))]
        public async Task<IHttpActionResult> PostT_clientes(T_clientes t_clientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_clientes.Add(t_clientes);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_clientes.Id_Cliente }, t_clientes);
        }

        // DELETE: api/T_clientes/5
        [ResponseType(typeof(T_clientes))]
        public async Task<IHttpActionResult> DeleteT_clientes(int id)
        {
            T_clientes t_clientes = await db.T_clientes.FindAsync(id);
            if (t_clientes == null)
            {
                return NotFound();
            }

            db.T_clientes.Remove(t_clientes);
            await db.SaveChangesAsync();

            return Ok(t_clientes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_clientesExists(int id)
        {
            return db.T_clientes.Count(e => e.Id_Cliente == id) > 0;
        }
    }
}