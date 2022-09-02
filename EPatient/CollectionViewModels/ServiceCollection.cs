using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPatient.Models;

namespace EPatient.CollectionViewModels
{
    public class ServiceCollection
    {
        public Service Service { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
    }
}