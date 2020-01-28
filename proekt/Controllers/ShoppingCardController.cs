using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using proekt.Models;
using proekt.ViewModels;
namespace proekt.Controllers
{   
    [Authorize]
    public class ShoppingCardController : Controller
    {
        ApplicationDbContext data = new ApplicationDbContext();
        // GET: ShoppingCard
        public ActionResult Index()
        {
           
            var card = CardModule.GetCard(this.HttpContext);
            var viewModel = new ShoppingViewModel
            {
                cardItems = card.GetAllCardItems(),
                cardTotla = card.GetTotal()
            };
            return View(viewModel);
        }

        public ActionResult AddToShoppingCard(int id)
        {
            var addItem = data.Telefons.Single(item => item.TelefonID == id);
            var card = CardModule.GetCard(this.HttpContext);
            card.AddToCard(addItem);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RemoveItem(int id)
        {
            var card = CardModule.GetCard(this.HttpContext);

            int c = card.RemoveItem(id);
            var model = new ShoppingRemoveViewModel
            {
                Total = card.GetTotal(),
                cardCount = card.GetCount(),
                ItemCount = c,
                DeleteId = id
            };
            return Json(model);
        }
    }
}