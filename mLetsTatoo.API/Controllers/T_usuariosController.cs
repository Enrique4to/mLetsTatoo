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
    public class T_usuariosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_usuarios
        public IQueryable<T_usuarios> GetT_usuarios()
        {
            return db.T_usuarios;
        }

        // GET: api/T_usuarios/5
        [ResponseType(typeof(T_usuarios))]
        public async Task<IHttpActionResult> GetT_usuarios(int id)
        {
            T_usuarios t_usuarios = await db.T_usuarios.FindAsync(id);
            if (t_usuarios == null)
            {
                return NotFound();
            }

            return Ok(t_usuarios);
        }

        // PUT: api/T_usuarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_usuarios(int id, T_usuarios t_usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_usuarios.Id_usuario)
            {
                return BadRequest();
            }

            db.Entry(t_usuarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_usuariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_usuarios);
        }

        // POST: api/T_usuarios
        [ResponseType(typeof(T_usuarios))]
        public async Task<IHttpActionResult> PostT_usuarios(T_usuarios t_usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_usuarios.Add(t_usuarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_usuarios.Id_usuario }, t_usuarios);
        }

        // DELETE: api/T_usuarios/5
        [ResponseType(typeof(T_usuarios))]
        public async Task<IHttpActionResult> DeleteT_usuarios(int id)
        {
            T_usuarios t_usuarios = await db.T_usuarios.FindAsync(id);
            if (t_usuarios == null)
            {
                return NotFound();
            }

            db.T_usuarios.Remove(t_usuarios);
            await db.SaveChangesAsync();

            return Ok(t_usuarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_usuariosExists(int id)
        {
            return db.T_usuarios.Count(e => e.Id_usuario == id) > 0;
        }
    }
}