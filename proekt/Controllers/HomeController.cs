using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proekt.ViewModels;
using System.Data;
using proekt.Models;

namespace proekt.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowAllTelefon(string sortOrder,string searchT) {
            TelefonViewModel t = new TelefonViewModel();
            t.telefoni = db.Telefons.ToList();
            if (!String.IsNullOrEmpty(searchT))
            {
                t.telefoni = t.telefoni.Where(s => s.ImeTelefon.Contains(searchT));
            }
            switch (sortOrder)
            {
                case "price":
                    t.telefoni = t.telefoni.OrderBy(a => a.cena);
                    break;
                case "price_desc":
                    t.telefoni = t.telefoni.OrderByDescending(a => a.cena);
                    break;
                case "ime":
                    t.telefoni = t.telefoni.OrderBy(a => a.ImeTelefon);
                    break;
                case "ime_desc":
                    t.telefoni = t.telefoni.OrderByDescending(a => a.ImeTelefon);
                    break;
                case "pro":
                    t.telefoni = t.telefoni.OrderBy(a => a.prozvoditel.ime);
                    break;

            }
            return View(t);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}