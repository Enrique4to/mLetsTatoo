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
    public class T_pagosclienteController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_pagoscliente
        public IQueryable<T_pagoscliente> GetT_pagoscliente()
        {
            return db.T_pagoscliente;
        }

        // GET: api/T_pagoscliente/5
        [ResponseType(typeof(T_pagoscliente))]
        public async Task<IHttpActionResult> GetT_pagoscliente(int id)
        {
            T_pagoscliente t_pagoscliente = await db.T_pagoscliente.FindAsync(id);
            if (t_pagoscliente == null)
            {
                return NotFound();
            }

            return Ok(t_pagoscliente);
        }

        // PUT: api/T_pagoscliente/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_pagoscliente(int id, T_pagoscliente t_pagoscliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_pagoscliente.Id_Pagocliente)
            {
                return BadRequest();
            }

            db.Entry(t_pagoscliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_pagosclienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_pagoscliente);
        }

        // POST: api/T_pagoscliente
        [ResponseType(typeof(T_pagoscliente))]
        public async Task<IHttpActionResult> PostT_pagoscliente(T_pagoscliente t_pagoscliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_pagoscliente.Add(t_pagoscliente);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_pagoscliente.Id_Pagocliente }, t_pagoscliente);
        }

        // DELETE: api/T_pagoscliente/5
        [ResponseType(typeof(T_pagoscliente))]
        public async Task<IHttpActionResult> DeleteT_pagoscliente(int id)
        {
            T_pagoscliente t_pagoscliente = await db.T_pagoscliente.FindAsync(id);
            if (t_pagoscliente == null)
            {
                return NotFound();
            }

            db.T_pagoscliente.Remove(t_pagoscliente);
            await db.SaveChangesAsync();

            return Ok(t_pagoscliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_pagosclienteExists(int id)
        {
            return db.T_pagoscliente.Count(e => e.Id_Pagocliente == id) > 0;
        }
    }
}