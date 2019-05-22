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
    public class T_pagostecnicoController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_pagostecnico
        public IQueryable<T_pagostecnico> GetT_pagostecnico()
        {
            return db.T_pagostecnico;
        }

        // GET: api/T_pagostecnico/5
        [ResponseType(typeof(T_pagostecnico))]
        public async Task<IHttpActionResult> GetT_pagostecnico(int id)
        {
            T_pagostecnico t_pagostecnico = await db.T_pagostecnico.FindAsync(id);
            if (t_pagostecnico == null)
            {
                return NotFound();
            }

            return Ok(t_pagostecnico);
        }

        // PUT: api/T_pagostecnico/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_pagostecnico(int id, T_pagostecnico t_pagostecnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_pagostecnico.Id_Pagotecnico)
            {
                return BadRequest();
            }

            db.Entry(t_pagostecnico).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_pagostecnicoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_pagostecnico);
        }

        // POST: api/T_pagostecnico
        [ResponseType(typeof(T_pagostecnico))]
        public async Task<IHttpActionResult> PostT_pagostecnico(T_pagostecnico t_pagostecnico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_pagostecnico.Add(t_pagostecnico);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_pagostecnico.Id_Pagotecnico }, t_pagostecnico);
        }

        // DELETE: api/T_pagostecnico/5
        [ResponseType(typeof(T_pagostecnico))]
        public async Task<IHttpActionResult> DeleteT_pagostecnico(int id)
        {
            T_pagostecnico t_pagostecnico = await db.T_pagostecnico.FindAsync(id);
            if (t_pagostecnico == null)
            {
                return NotFound();
            }

            db.T_pagostecnico.Remove(t_pagostecnico);
            await db.SaveChangesAsync();

            return Ok(t_pagostecnico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_pagostecnicoExists(int id)
        {
            return db.T_pagostecnico.Count(e => e.Id_Pagotecnico == id) > 0;
        }
    }
}