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
    public class T_balancetecnicoController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_balancetecnico
        public IQueryable<T_balancetecnico> GetT_balancetecnico()
        {
            return db.T_balancetecnico;
        }

        // GET: api/T_balancetecnico/5
        [ResponseType(typeof(T_balancetecnico))]
        public async Task<IHttpActionResult> GetT_balancetecnico(int id)
        {
            T_balancetecnico t_balancetecnico = await db.T_balancetecnico.FindAsync(id);
            if (t_balancetecnico == null)
            {
                return NotFound();
            }

            return Ok(t_balancetecnico);
        }

        // PUT: api/T_balancetecnico/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_balancetecnico(int id, T_balancetecnico t_balancetecnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_balancetecnico.Id_Balancetecnico)
            {
                return BadRequest();
            }

            db.Entry(t_balancetecnico).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_balancetecnicoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_balancetecnico);
        }

        // POST: api/T_balancetecnico
        [ResponseType(typeof(T_balancetecnico))]
        public async Task<IHttpActionResult> PostT_balancetecnico(T_balancetecnico t_balancetecnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_balancetecnico.Add(t_balancetecnico);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_balancetecnico.Id_Balancetecnico }, t_balancetecnico);
        }

        // DELETE: api/T_balancetecnico/5
        [ResponseType(typeof(T_balancetecnico))]
        public async Task<IHttpActionResult> DeleteT_balancetecnico(int id)
        {
            T_balancetecnico t_balancetecnico = await db.T_balancetecnico.FindAsync(id);
            if (t_balancetecnico == null)
            {
                return NotFound();
            }

            db.T_balancetecnico.Remove(t_balancetecnico);
            await db.SaveChangesAsync();

            return Ok(t_balancetecnico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_balancetecnicoExists(int id)
        {
            return db.T_balancetecnico.Count(e => e.Id_Balancetecnico == id) > 0;
        }
    }
}