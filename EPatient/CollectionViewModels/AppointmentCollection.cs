using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPatient.Models;

namespace EPatient.CollectionViewModels
{
    public class AppointmentCollection
    {
        public Appointment Appointment { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
    }
}