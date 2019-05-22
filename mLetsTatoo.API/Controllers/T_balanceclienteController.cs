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
    public class T_balanceclienteController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_balancecliente
        public IQueryable<T_balancecliente> GetT_balancecliente()
        {
            return db.T_balancecliente;
        }

        // GET: api/T_balancecliente/5
        [ResponseType(typeof(T_balancecliente))]
        public async Task<IHttpActionResult> GetT_balancecliente(int id)
        {
            T_balancecliente t_balancecliente = await db.T_balancecliente.FindAsync(id);
            if (t_balancecliente == null)
            {
                return NotFound();
            }

            return Ok(t_balancecliente);
        }

        // PUT: api/T_balancecliente/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_balancecliente(int id, T_balancecliente t_balancecliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_balancecliente.Id_Balancecliente)
            {
                return BadRequest();
            }

            db.Entry(t_balancecliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_balanceclienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_balancecliente);
        }

        // POST: api/T_balancecliente
        [ResponseType(typeof(T_balancecliente))]
        public async Task<IHttpActionResult> PostT_balancecliente(T_balancecliente t_balancecliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_balancecliente.Add(t_balancecliente);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_balancecliente.Id_Balancecliente }, t_balancecliente);
        }

        // DELETE: api/T_balancecliente/5
        [ResponseType(typeof(T_balancecliente))]
        public async Task<IHttpActionResult> DeleteT_balancecliente(int id)
        {
            T_balancecliente t_balancecliente = await db.T_balancecliente.FindAsync(id);
            if (t_balancecliente == null)
            {
                return NotFound();
            }

            db.T_balancecliente.Remove(t_balancecliente);
            await db.SaveChangesAsync();

            return Ok(t_balancecliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_balanceclienteExists(int id)
        {
            return db.T_balancecliente.Count(e => e.Id_Balancecliente == id) > 0;
        }
    }
}