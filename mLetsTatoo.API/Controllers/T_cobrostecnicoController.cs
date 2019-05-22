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
    public class T_cobrostecnicoController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_cobrostecnico
        public IQueryable<T_cobrostecnico> GetT_cobrostecnico()
        {
            return db.T_cobrostecnico;
        }

        // GET: api/T_cobrostecnico/5
        [ResponseType(typeof(T_cobrostecnico))]
        public async Task<IHttpActionResult> GetT_cobrostecnico(int id)
        {
            T_cobrostecnico t_cobrostecnico = await db.T_cobrostecnico.FindAsync(id);
            if (t_cobrostecnico == null)
            {
                return NotFound();
            }

            return Ok(t_cobrostecnico);
        }

        // PUT: api/T_cobrostecnico/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_cobrostecnico(int id, T_cobrostecnico t_cobrostecnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_cobrostecnico.Id_Cobrotecnico)
            {
                return BadRequest();
            }

            db.Entry(t_cobrostecnico).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_cobrostecnicoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_cobrostecnico);
        }

        // POST: api/T_cobrostecnico
        [ResponseType(typeof(T_cobrostecnico))]
        public async Task<IHttpActionResult> PostT_cobrostecnico(T_cobrostecnico t_cobrostecnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_cobrostecnico.Add(t_cobrostecnico);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_cobrostecnico.Id_Cobrotecnico }, t_cobrostecnico);
        }

        // DELETE: api/T_cobrostecnico/5
        [ResponseType(typeof(T_cobrostecnico))]
        public async Task<IHttpActionResult> DeleteT_cobrostecnico(int id)
        {
            T_cobrostecnico t_cobrostecnico = await db.T_cobrostecnico.FindAsync(id);
            if (t_cobrostecnico == null)
            {
                return NotFound();
            }

            db.T_cobrostecnico.Remove(t_cobrostecnico);
            await db.SaveChangesAsync();

            return Ok(t_cobrostecnico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_cobrostecnicoExists(int id)
        {
            return db.T_cobrostecnico.Count(e => e.Id_Cobrotecnico == id) > 0;
        }
    }
}