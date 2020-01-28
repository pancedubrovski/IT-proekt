using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proekt.Models
{
    public class ShoppingItme
    {
        public int ItemId { get; set; }
        public int Amount { get; set; }
        public string ShoppingCardId { get; set; }
        public int TelefonId { get; set; }
        public virtual Telefon Telefon { get; set; }
    }
}