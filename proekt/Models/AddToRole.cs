using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proekt.Models
{
    public class AddToRole
    {
          [Required]
          public string Email { get; set; }
          public string Role { get; set; }
          public List<string> roles { get; set; }
         
        

    }
}
