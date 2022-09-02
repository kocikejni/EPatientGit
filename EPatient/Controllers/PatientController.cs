using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPatient.CollectionViewModels;
using EPatient.Models;
using Microsoft.AspNet.Identity;
using Rotativa;

namespace EPatient.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext db;

        //Constructor
        public PatientController()
        {
            db = new ApplicationDbContext();
        }

        //Destructor
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        [Authorize(Roles = "Patient")]
        public ActionResult Index(string message)
        {
            ViewBag.Messege = message;
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var date = DateTime.Now.Date;
            var model = new CollectionOfAll
            {
                Departments = db.Department.ToList(),
                Doctors = db.Doctors.ToList(),
                Patients = db.Patients.ToList(),
                Services = db.Services.ToList(),
                ActiveAppointments = db.Appointments.Where(c => c.Status).Where(c => c.PatientId == patient.Id).Where(c => c.AppointmentDate >= date).ToList(),
                PendingAppointments = db.Appointments.Where(c => c.Status == false).Where(c => c.PatientId == patient.Id).Where(c => c.AppointmentDate >= date).ToList()
            };
            return View(model);
        }

        //Update Patient profile
        [Authorize(Roles = "Patient")]
        public ActionResult UpdateProfile(string id)
        {
            var patient = db.Patients.Single(c => c.ApplicationUserId == id);
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(string id, Patient model)
        {
            var patient = db.Patients.Single(c => c.ApplicationUserId == id);
            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;
            patient.FullName = model.FirstName + " " + model.LastName;
            patient.Contact = model.Contact;
            patient.Address = model.Address;
            patient.BloodGroup = model.BloodGroup;
            patient.DateOfBirth = model.DateOfBirth;
            patient.Gender = model.Gender;
            patient.PhoneNo = model.PhoneNo;
            patient.Allergies = model.Allergies;
            db.SaveChanges();
            return View();
        }

        [Authorize(Roles = "Patient")]
        public ActionResult PatientDetail(string id)
        {
            var patient = db.Patients.SingleOrDefault(c => c.ApplicationUserId == id);
            return View(patient);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult DownloadPatientDetails()
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            return View(from Prescription in db.Prescription.Take(10)
                        select Prescription);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult PrintPartialViewOfPatientDetailsToPdf(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Patient patient = db.Patients.FirstOrDefault(c => c.Id == id);

                var report = new PartialViewAsPdf("~/Views/Patient/PatientDetailsPdf.cshtml", patient);
                return report;
            }
        }

        //Add Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult AddAppointment()
        {
            var collection = new AppointmentCollection
            {
                Appointment = new Appointment(),
                Doctors = db.Doctors.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAppointment(AppointmentCollection model)
        {
            var collection = new AppointmentCollection
            {
                Appointment = model.Appointment,
                Doctors = db.Doctors.ToList()
            };
            if (model.Appointment.AppointmentDate >= DateTime.Now.Date)
            {
                string user = User.Identity.GetUserId();
                var patient = db.Patients.Single(c => c.ApplicationUserId == user);
                var appointment = new Appointment();
                appointment.PatientId = patient.Id;
                appointment.DoctorId = model.Appointment.DoctorId;
                appointment.AppointmentDate = model.Appointment.AppointmentDate;
                appointment.Problem = model.Appointment.Problem;
                appointment.Status = false;

                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("ListOfAppointments");
            }
            ViewBag.Messege = "Please Enter the Date greater than today or equal!!";

            return View(collection);

        }

        //List of Appointments
        [Authorize(Roles = "Patient")]
        public ActionResult ListOfAppointments()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var appointment = db.Appointments.Include(c => c.Doctor).Where(c => c.PatientId == patient.Id).ToList();
            return View(appointment);
        }
        //Download Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult PrintPartialViewOfAppointmentsToPdf(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Appointment appointment = db.Appointments.FirstOrDefault(c => c.Id == id);

                var report = new PartialViewAsPdf("~/Views/Doctor/DetailOfAppointment.cshtml", appointment);
                return report;
            }

        }
        //Edit Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult EditAppointment(int id)
        {
            var collection = new AppointmentCollection
            {
                Appointment = db.Appointments.Single(c => c.Id == id),
                Doctors = db.Doctors.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAppointment(int id, AppointmentCollection model)
        {
            var collection = new AppointmentCollection
            {
                Appointment = model.Appointment,
                Doctors = db.Doctors.ToList()
            };
            if (model.Appointment.AppointmentDate >= DateTime.Now.Date)
            {
                var appointment = db.Appointments.Single(c => c.Id == id);
                appointment.DoctorId = model.Appointment.DoctorId;
                appointment.AppointmentDate = model.Appointment.AppointmentDate;
                appointment.Problem = model.Appointment.Problem;
                db.SaveChanges();
                return RedirectToAction("ListOfAppointments");
            }
            ViewBag.Messege = "Please Enter the Date greater than today or equal!!";

            return View(collection);
        }

        //Delete Appointment
        [Authorize(Roles = "Patient")]
        public ActionResult DeleteAppointment(int? id)
        {
            var appointment = db.Appointments.Single(c => c.Id == id);
            return View(appointment);
        }

        [HttpPost, ActionName("DeleteAppointment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAppointment(int id)
        {
            var appointment = db.Appointments.Single(c => c.Id == id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("ListOfAppointments");
        }

        //End Appointment Section

        //Start Doctor Section

        //List of Available Doctors
        [Authorize(Roles = "Patient")]
        public ActionResult AvailableDoctors()
        {
            var doctor = db.Doctors.Include(c => c.Department).Where(c => c.Status == "Active").ToList();
            return View(doctor);
        }


        //Doctor Detail
        [Authorize(Roles = "Patient")]
        public ActionResult DoctorDetail(int id)
        {
            var doctor = db.Doctors.Include(c => c.Department).Single(c => c.Id == id);
            return View(doctor);
        }

        //Start Prescription Section

        //List of Prescription
        [Authorize(Roles = "Patient")]
        public ActionResult ListOfPrescription()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var prescription = db.Prescription.Include(c => c.Doctor).Where(c => c.PatientId == patient.Id).ToList();
            return View(prescription);
        }

        //Prescription View
        [Authorize(Roles = "Patient")]
        public ActionResult PrescriptionView(int id)
        {
            var prescription = db.Prescription.Single(c => c.Id == id);
            return View(prescription);
        }

        //Download Prescription
        [Authorize(Roles = "Patient")]
        public ActionResult DownloadPrescription()
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            return View(from Prescription in db.Prescription.Take(10)
                        select Prescription);
        }

        [Authorize(Roles = "Patient")]
        public ActionResult PrintPartialViewOfPrescriptionToPdf(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Prescription prescription = db.Prescription.FirstOrDefault(c => c.Id == id);

                var report = new PartialViewAsPdf("~/Views/Patient/PrescriptionView.cshtml", prescription);
                return report;
            }
        }
            //Viewing Images
            [Authorize(Roles = "Patient")]
        public ActionResult ListOfImages()
        {
            string user = User.Identity.GetUserId();
            var patient = db.Patients.Single(c => c.ApplicationUserId == user);
            var images = db.Images.Where(c => c.PatientFullName == patient.FullName).ToList();
            return View(images);
        }
        [HttpGet]
        [Authorize(Roles = "Patient")]

        public ActionResult ViewImage(int id)
        {
            Image imageModel = new Image();

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                imageModel = db.Images.Where(x => x.Id == id).FirstOrDefault();
            }

            return View(imageModel);
        }

        //Service Section
        [Authorize(Roles = "Patient")]
        public ActionResult ListOfServices()
        {
            var service = db.Services.Include(c => c.Doctor).ToList();
            return View(service);
        }

        


    }
}