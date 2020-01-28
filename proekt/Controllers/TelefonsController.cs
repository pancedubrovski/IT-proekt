using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using proekt.Models;

namespace proekt.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class TelefonsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Telefons
        public IQueryable<Telefon> GetTelefons()
        {
            return db.Telefons;
        }

        // GET: api/Telefons/5
        [ResponseType(typeof(Telefon))]
        public IHttpActionResult GetTelefon(int id)
        {
            Telefon telefon = db.Telefons.Find(id);
            if (telefon == null)
            {
                return NotFound();
            }

            return Ok(telefon);
        }

        // PUT: api/Telefons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTelefon(int id, Telefon telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != telefon.TelefonID)
            {
                return BadRequest();
            }

            db.Entry(telefon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonExists(id))
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

        // POST: api/Telefons
        [ResponseType(typeof(Telefon))]
        public IHttpActionResult PostTelefon(Telefon telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Telefons.Add(telefon);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = telefon.TelefonID }, telefon);
        }

        // DELETE: api/Telefons/5
        [ResponseType(typeof(Telefon))]
        public IHttpActionResult Delete(int id)
        {
            Telefon telefon = db.Telefons.Find(id);
            if (telefon == null)
            {
                return NotFound();
            }

            db.Telefons.Remove(telefon);
            db.SaveChanges();

            return Ok(telefon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TelefonExists(int id)
        {
            return db.Telefons.Count(e => e.TelefonID == id) > 0;
        }
    }
}