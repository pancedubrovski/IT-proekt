using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace proekt.Models
{
    [Bind(Exclude="OrderId")]
    public class Order
    {
        [Key]
         public int OrderId { get; set; }

        [ScaffoldColumn(false)] public string Username { get; set; }

  
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        public string Country { get; set; }

        [Phone] public string Phone { get; set; }

        
        [Display(Name = "Email Address")]
            
        public string Email { get; set; }

        [ScaffoldColumn(false)] public decimal Total { get; set; }

        [ScaffoldColumn(false)] public DateTime OrderDate { get; set; }

        public ICollection<OrederDetals> OrderDetails { get; set; }

       
    }
}
