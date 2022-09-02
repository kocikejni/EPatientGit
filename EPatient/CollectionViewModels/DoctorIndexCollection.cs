using EPatient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPatient.CollectionViewModels
{
    public class DoctorIndexCollection
    {
        public List<Appointment> Appointments { get; set; }
    }
}