using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proekt.Models
{
    public class CardModule
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string CardId { get; set; }
        public const string CardSessionKey = "CardId";
       
        public static CardModule GetCard(HttpContextBase contex) {
            var card = new CardModule();
            card.CardId = card.GetCardId(contex);
            return card;
        }
        public string GetCardId(HttpContextBase contex)
        {
            if (contex.Session[CardSessionKey] == null) {
                if (!string.IsNullOrWhiteSpace(contex.User.Identity.Name)) {
                    contex.Session[CardSessionKey] = contex.User.Identity.Name;
                }
                else
                {
                    Guid tempCardId = Guid.NewGuid();
                    contex.Session[CardSessionKey] = tempCardId.ToString();
                }
            }
            return contex.Session[CardSessionKey].ToString();
        }
        public static CardModule GetCard(Controller controller)
        {
            return GetCard(controller.HttpContext);
        }
        public void AddToCard(Telefon telefon) {
            var cardItem = db.Cards.FirstOrDefault(c => c.cardId == CardId && c.TelefonId == telefon.TelefonID);

            if (cardItem == null)
            {
                cardItem = new Card
                {
                    cardId = CardId,
                    TelefonId = telefon.TelefonID,
                    Count = 1,
                    DataCreate = DateTime.Now

                };
                db.Cards.Add(cardItem);
            }
            else {
                cardItem.Count++;
            }
            db.SaveChanges();
        }
        public int RemoveItem(int id) {
            int countItem = 0;
            var cardItem = db.Cards.FirstOrDefault(c => c.cardId == CardId && c.Recordid == id);
            if (cardItem != null) {
                if (cardItem.Count > 1)
                {
                    countItem = --cardItem.Count;
                }
                else {
                    db.Cards.Remove(cardItem);
                }
                db.SaveChanges();
            }
            return countItem;
        }
        public void EmpetyCard() {
            var cardItems = db.Cards.Where(c => c.cardId == CardId);
            foreach (var cardItem in cardItems) {
                db.Cards.Remove(cardItem);
            }
            db.SaveChanges();
        }
        public List<Card> GetAllCardItems()
        {
            return db.Cards.Where(c => c.cardId == CardId).ToList();
        }
        public int GetCount()
        {
            int? count = GetAllCardItems().Count;
            return count ?? 0;
        }
        public decimal GetTotal() {
            decimal? total = GetAllCardItems().Sum(c => c.Count * c.Telefon.cena);
            return total ?? decimal.Zero;
        }
        public int CreateOreder(Order order) {
            order.Total = GetTotal();
            order.OrderDate = DateTime.Now;
            db.Orders.Add(order);
            
            var cartItems = GetAllCardItems();
            foreach(var cardItem in cartItems)
            {
                var oredrDetals = new OrederDetals(order.OrderId, cardItem.TelefonId, cardItem.Telefon.cena, cardItem.Count);
          
                db.OrederDetals.Add(oredrDetals);
            }
            db.SaveChanges();
            EmpetyCard();
            return order.OrderId;    
        }
        


    }

}
