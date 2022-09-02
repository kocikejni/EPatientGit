using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EPatient.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public Patient Patient { get; set; }
        [Display(Name = "Patient Name")]
        public string PatientFullName { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}