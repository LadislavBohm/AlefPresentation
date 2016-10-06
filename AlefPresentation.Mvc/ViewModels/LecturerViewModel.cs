using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlefPresentation.Mvc.ViewModels
{
    public class LecturerViewModel
    {
        [Display(Name = "Jmeno"), Required, MaxLength(20)]
        public string FirstName { get; set; }
        [Display(Name = "Prijmeni"), Required, MinLength(10)]
        public string LastName { get; set; }
        [Display(Name = "Email"), Required, EmailAddress]
        public string Email { get; set; }

        public bool? OperationSuccessful { get; set; }
    }
}