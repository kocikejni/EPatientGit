using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPatient.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public Doctor Doctor { get; set; }
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }
        public string Price { get; set; }
    }
}