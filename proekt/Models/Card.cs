using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Linq;

namespace proekt.Models
{
    public class Card
    {
        [Key]
        [Required]
        public int Recordid { get; set; }
        [Required]
        public string cardId { get; set; }
        [Required]
        public int  TelefonId { get; set; }
        public int Count { get; set; }
        public DateTime DataCreate { get; set; }
        public virtual Telefon Telefon { get; set; }
    }
}
