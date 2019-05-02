

namespace mLetsTatoo.API.Controllers
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using mLetsTatoo.Domain.Modules;
    using mLetsTatoo.Models;
    public class T_citaimagenestempController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/T_citaimagenestemp
        public IQueryable<T_citaimagenestemp> GetT_citaimagenestemp()
        {
            return db.T_citaimagenestemp;
        }

        // GET: api/T_citaimagenestemp/5
        [ResponseType(typeof(T_citaimagenestemp))]
        public async Task<IHttpActionResult> GetT_citaimagenestemp(int id)
        {
            T_citaimagenestemp t_citaimagenestemp = await db.T_citaimagenestemp.FindAsync(id);
            if (t_citaimagenestemp == null)
            {
                return NotFound();
            }

            return Ok(t_citaimagenestemp);
        }

        // PUT: api/T_citaimagenestemp/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutT_citaimagenestemp(int id, T_citaimagenestemp t_citaimagenestemp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_citaimagenestemp.Id_Imagentemp)
            {
                return BadRequest();
            }

            db.Entry(t_citaimagenestemp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!T_citaimagenestempExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(t_citaimagenestemp);
        }

        // POST: api/T_citaimagenestemp
        [ResponseType(typeof(T_citaimagenestemp))]
        public async Task<IHttpActionResult> PostT_citaimagenestemp(T_citaimagenestemp t_citaimagenestemp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.T_citaimagenestemp.Add(t_citaimagenestemp);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = t_citaimagenestemp.Id_Imagentemp }, t_citaimagenestemp);
        }

        // DELETE: api/T_citaimagenestemp/5
        [ResponseType(typeof(T_citaimagenestemp))]
        public async Task<IHttpActionResult> DeleteT_citaimagenestemp(int id)
        {
            T_citaimagenestemp t_citaimagenestemp = await db.T_citaimagenestemp.FindAsync(id);
            if (t_citaimagenestemp == null)
            {
                return NotFound();
            }

            db.T_citaimagenestemp.Remove(t_citaimagenestemp);
            await db.SaveChangesAsync();

            return Ok(t_citaimagenestemp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool T_citaimagenestempExists(int id)
        {
            return db.T_citaimagenestemp.Count(e => e.Id_Imagentemp == id) > 0;
        }
    }
}