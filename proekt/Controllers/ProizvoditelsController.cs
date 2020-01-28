using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proekt.Models;

namespace proekt.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class ProizvoditelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Proizvoditels
        public ActionResult Index()
        {
            return View(db.Proizvoditels.ToList());
        }

        public ActionResult Phone(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Telefon> telefoni = new List<Telefon>();
            List<Telefon> telefons = db.Telefons.ToList();
            foreach (Telefon t in telefons) {
                if (t.proID == id) {
                    telefoni.Add(t);
                }
            }
            return View(telefoni);
        }

        // GET: Proizvoditels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvoditel proizvoditel = db.Proizvoditels.Find(id);
            if (proizvoditel == null)
            {
                return HttpNotFound();
            }
            return View(proizvoditel);
        }

        // GET: Proizvoditels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proizvoditels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "proID,ime")] Proizvoditel proizvoditel)
        {
            if (ModelState.IsValid)
            {
                db.Proizvoditels.Add(proizvoditel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proizvoditel);
        }

        // GET: Proizvoditels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvoditel proizvoditel = db.Proizvoditels.Find(id);
            if (proizvoditel == null)
            {
                return HttpNotFound();
            }
            return View(proizvoditel);
        }

        // POST: Proizvoditels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "proID,ime")] Proizvoditel proizvoditel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvoditel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proizvoditel);
        }

        // GET: Proizvoditels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvoditel proizvoditel = db.Proizvoditels.Find(id);
            if (proizvoditel == null)
            {
                return HttpNotFound();
            }
            return View(proizvoditel);
        }

        // POST: Proizvoditels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proizvoditel proizvoditel = db.Proizvoditels.Find(id);
            db.Proizvoditels.Remove(proizvoditel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
