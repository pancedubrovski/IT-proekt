using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace proekt.Models
{
    public class Telefon
    {
        [Key]
        public int TelefonID { get; set; }
        [Required]
        [Display(Name ="Model")]
        public string ImeTelefon { get; set; }
        [Required]
        [Display(Name ="Price")]      
        public int cena { get; set; }
        [Required]
        [Display(Name ="Screen")]
        public float ekran { get; set; }
        [Required]
        [Display(Name ="Cpu")]
        public String procesor { get; set; }
        [Required]
        public int RAM { get; set; }
        [Required]
        [Display(Name ="Camera")]
        public int kamera { get; set; }
        public string slika { get; set; }
        public int proID { get; set; }
       
        public virtual Proizvoditel prozvoditel { get; set; }
    }
}
