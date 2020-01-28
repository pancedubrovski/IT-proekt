using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace proekt.Models
{
    public class OrederDetals 
    {
       
        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int TelefonId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public virtual Telefon Telefon { get; set; }

        public virtual Order Order { get; set; }
        public OrederDetals(int oi,int ti,int quan,decimal unitPrice) {
            this.OrderId = oi;
            this.TelefonId = ti;
            this.Quantity = quan;
            this.UnitPrice = unitPrice;
        }
    }
}
