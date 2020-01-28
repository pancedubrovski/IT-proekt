using System;
using System.Web;

namespace proekt.ViewModels
{
    public class ShoppingRemoveViewModel 
    {
        public string  messange { get; set; }
        public decimal Total { get; set; }
        public int cardCount { get; set; }
        public int  ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
}
