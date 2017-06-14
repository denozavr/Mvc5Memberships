using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc5Memberships.Models
{
    public class UserViewModel
    {
        [Display(Name = "User Id")]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = "First Name")]
        [StringLength(30, ErrorMessage = "The {0} should be at least {1} chars long", MinimumLength = 2)]
        public string FirstName { get; set; }


        [Required]
        [StringLength(70, ErrorMessage = "The {0} should be at least {1} chars long and have at least 1 digit.s", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}