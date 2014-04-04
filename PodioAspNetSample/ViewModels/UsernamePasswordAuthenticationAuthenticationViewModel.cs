using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PodioAspNetSample.ViewModels
{
    public class UsernamePasswordAuthenticationViewModel
    {
        [Required]
        [Display(Name="Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}