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
    [Authorize]
    public class OrederDetalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrederDetals
        public ActionResult Index()
        {
            var orederDetals = db.OrederDetals.Include(o => o.Order).Include(o => o.Telefon);
            return View(orederDetals.ToList());
        }

        // GET: OrederDetals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrederDetals orederDetals = db.OrederDetals.Find(id);
            if (orederDetals == null)
            {
                return HttpNotFound();
            }
            return View(orederDetals);
        }

        // GET: OrederDetals/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username");
            ViewBag.TelefonId = new SelectList(db.Telefons, "TelefonID", "ImeTelefon");
            return View();
        }

        // POST: OrederDetals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetailId,OrderId,TelefonId,Quantity,UnitPrice")] OrederDetals orederDetals)
        {
            if (ModelState.IsValid)
            {
                db.OrederDetals.Add(orederDetals);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orederDetals.OrderId);
            ViewBag.TelefonId = new SelectList(db.Telefons, "TelefonID", "ImeTelefon", orederDetals.TelefonId);
            return View(orederDetals);
        }

        // GET: OrederDetals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrederDetals orederDetals = db.OrederDetals.Find(id);
            if (orederDetals == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orederDetals.OrderId);
            ViewBag.TelefonId = new SelectList(db.Telefons, "TelefonID", "ImeTelefon", orederDetals.TelefonId);
            return View(orederDetals);
        }

        // POST: OrederDetals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailId,OrderId,TelefonId,Quantity,UnitPrice")] OrederDetals orederDetals)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orederDetals).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orederDetals.OrderId);
            ViewBag.TelefonId = new SelectList(db.Telefons, "TelefonID", "ImeTelefon", orederDetals.TelefonId);
            return View(orederDetals);
        }

        // GET: OrederDetals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrederDetals orederDetals = db.OrederDetals.Find(id);
            if (orederDetals == null)
            {
                return HttpNotFound();
            }
            return View(orederDetals);
        }

        // POST: OrederDetals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrederDetals orederDetals = db.OrederDetals.Find(id);
            db.OrederDetals.Remove(orederDetals);
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
