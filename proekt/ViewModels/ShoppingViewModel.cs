using System;
using System.Web;
using System.Collections.Generic;
using proekt.Models;
namespace proekt.ViewModels
{
    public class ShoppingViewModel 
    {
       

        public List<Card> cardItems { get; set; }
        public decimal cardTotla { get; set; }
    }
}
