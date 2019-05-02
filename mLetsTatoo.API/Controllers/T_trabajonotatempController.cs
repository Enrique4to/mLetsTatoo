namespace mLetsTatoo.API.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using mLetsTatoo.Domain.Modules;
    using mLetsTatoo.Models;
    public class T_trabajonotatempController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_trabajonotatemp
        public IQueryable<T_trabajonotatemp> GetT_trabajonotatemp()
        {
            return db.T_trabajonotatemp;
        }

        // GET: api/T_trabajonotatemp/5
        [ResponseType(typeof(T_trabajonotatemp))]
        public async Task<IHttpActionResult> GetT_trabajonotatemp(int id)
        {
            T_trabajonotatemp t_trabajonotatemp = await db.T_trabajonotatemp.FindAsync(id);
            if (t_trabajonotatemp == null)
            {
                return NotFound();
            }

            return Ok(t_trabajonotatemp);
        }

        // PUT: api/T_trabajonotatemp/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_trabajonotatemp(int id, T_trabajonotatemp t_trabajonotatemp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_trabajonotatemp.Id_Notatemp)
            {
                return BadRequest();
            }

            db.Entry(t_trabajonotatemp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_trabajonotatempExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_trabajonotatemp);
        }

        // POST: api/T_trabajonotatemp
        [ResponseType(typeof(T_trabajonotatemp))]
        public async Task<IHttpActionResult> PostT_trabajonotatemp(T_trabajonotatemp t_trabajonotatemp)
        {
            t_trabajonotatemp.F_nota = DateTime.Now.ToUniversalTime();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_trabajonotatemp.Add(t_trabajonotatemp);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_trabajonotatemp.Id_Notatemp }, t_trabajonotatemp);
        }

        // DELETE: api/T_trabajonotatemp/5
        [ResponseType(typeof(T_trabajonotatemp))]
        public async Task<IHttpActionResult> DeleteT_trabajonotatemp(int id)
        {
            T_trabajonotatemp t_trabajonotatemp = await db.T_trabajonotatemp.FindAsync(id);
            if (t_trabajonotatemp == null)
            {
                return NotFound();
            }

            db.T_trabajonotatemp.Remove(t_trabajonotatemp);
            await db.SaveChangesAsync();

            return Ok(t_trabajonotatemp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_trabajonotatempExists(int id)
        {
            return db.T_trabajonotatemp.Count(e => e.Id_Notatemp == id) > 0;
        }
    }
}