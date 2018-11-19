using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        public int EntityId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage ="Passwords do not mutch.")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}