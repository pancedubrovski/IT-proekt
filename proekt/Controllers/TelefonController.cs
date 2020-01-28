using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proekt.Models;
using proekt.ViewModels;

namespace proekt.Controllers
{
    public class TelefonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        public ActionResult Search(String s) {
            TelefonViewModel model = new TelefonViewModel();
            model.telefoni = db.Telefons.Where(t => s == null || t.ImeTelefon.StartsWith(s)).ToList();

            return View(model);
        }
        // GET: Telefon
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Index(string sortOrder)
        {
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";

            var telefon = from t in db.Telefons
                          select t;
           
            switch (sortOrder) {
                case "price":
                     telefon = telefon.OrderBy(a => a.cena);
                    break;
                case "price_desc":
                    telefon = telefon.OrderByDescending(a => a.cena);
                    break;
                case "ime":
                    telefon = telefon.OrderBy(a => a.ImeTelefon);
                    break;
                case "ime_desc":
                    telefon = telefon.OrderByDescending(a => a.ImeTelefon);
                    break;
                case "pro":
                    telefon = telefon.OrderBy(a => a.prozvoditel.ime);
                    break;
                case "ekran":
                    telefon = telefon.OrderBy(a => a.ekran);
                    break;
                
            }
            
            return View(telefon.ToList());
        }

        // GET: Telefon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefon telefon = db.Telefons.Find(id);
            if (telefon == null)
            {
                return HttpNotFound();
            }
            return View(telefon);
        }

        // GET: Telefon/Create
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create()
        {
            ViewBag.proID = new SelectList(db.Proizvoditels, "proID", "ime");
            return View();
        }

        // POST: Telefon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Create([Bind(Include = "TelefonID,ImeTelefon,cena,ekran,procesor,RAM,kamerea,slika,proID")] Telefon telefon)
        {
            if (ModelState.IsValid)
            {
                db.Telefons.Add(telefon);
                Proizvoditel por = db.Proizvoditels.Find(telefon.proID);
                por.AddPhone(telefon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.proID = new SelectList(db.Proizvoditels, "proID", "ime", telefon.proID);
            return View(telefon);
        }

        // GET: Telefon/Edit/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefon telefon = db.Telefons.Find(id);
            if (telefon == null)
            {
                return HttpNotFound();
            }
            ViewBag.proID = new SelectList(db.Proizvoditels, "proID", "ime", telefon.proID);
            return View(telefon);
        }

        // POST: Telefon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Edit([Bind(Include = "TelefonID,ImeTelefon,cena,ekran,procesor,RAM,kamera,slika,proID")] Telefon telefon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.proID = new SelectList(db.Proizvoditels, "proID", "ime", telefon.proID);
            return View(telefon);
        }

        // GET: Telefon/Delete/5
        [Authorize(Roles = "Admin,Editor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefon telefon = db.Telefons.Find(id);
            if (telefon == null)
            {
                return HttpNotFound();
            }
            return View(telefon);
        }

        // POST: Telefon/Delete/5
        [Authorize(Roles = "Admin,Editor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telefon telefon = db.Telefons.Find(id);
            db.Telefons.Remove(telefon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Editor")]
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
