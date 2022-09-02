using EPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPatient.CollectionViewModels
{
    public class PrescriptionCollection
    {
        public Prescription Prescription { get; set; }
        public List<Patient> Patients { get; set; }
    }
}