using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace proekt.Models
{
    public class Proizvoditel 
    {
        [Key]
        public int proID { get; set; }
        public string ime { get; set; }
        public List<Telefon> telefoni { get; set; }

        public void AddPhone(Telefon t)
        {
            this.telefoni.Add(t);

        }
    }
}
