using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlefPresentation.Api.Model
{
    public class NewAttendeeViewModel
    {
        [Required, StringLength(30)]
        public string FullName { get; set; }
        [Required, StringLength(30)]
        public string JobPosition { get; set; }
    }
}