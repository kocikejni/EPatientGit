using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPatient.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPatient.CollectionViewModels
{
    public class ImageCollection
    {
        public Image Image { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
    }
}