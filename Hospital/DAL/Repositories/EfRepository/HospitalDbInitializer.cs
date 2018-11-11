using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital.DAL.Repository.EfRepository
{

    public class HospitalDbInitializer : CreateDatabaseIfNotExists<HospitalContext>//DropCreateDatabaseAlways<HospitalContext> // 
    {
        protected override void Seed(HospitalContext context)
        {
            //specializations
            var surg = context.Specializations
                .Add(new Specialization { Name = "Surgeon" });
            var neurosurg = context.Specializations
                .Add(new Specialization { Name = "Neurological surgeon" });
            var orthosurg = context.Specializations
                .Add(new Specialization { Name = "Orthopedic surgeon" });
            var plasticsurg = context.Specializations
                .Add(new Specialization { Name = "Plastic surgeon" });
            var vascularsurg = context.Specializations
                .Add(new Specialization { Name = "Vascular surgeon" });
            var handsurg = context.Specializations
                .Add(new Specialization { Name = "Hand surgeon" });
            var cardiovascsurg = context.Specializations
                .Add(new Specialization { Name = "Cardiovascular surgeon" });
            var allerg = context.Specializations
                .Add(new Specialization { Name = "Allergist" });
            var anest = context.Specializations
                .Add(new Specialization { Name = "Anesthesiologist" });
            var cardio = context.Specializations
                .Add(new Specialization { Name = "Cardiologist" });
            var derma = context.Specializations
                .Add(new Specialization { Name = "Dermatologist" });
            var pediatr = context.Specializations
                .Add(new Specialization { Name = "Pediatrician" });
            var emerg = context.Specializations
                .Add(new Specialization { Name = "Emergency Medicine Specialist" });
            var familyPhys = context.Specializations
                .Add(new Specialization { Name = "Family Medicine Physician" });
            var gastro = context.Specializations
                .Add(new Specialization { Name = "Gastroenterologist" });
            var gynec = context.Specializations
                .Add(new Specialization { Name = "Gynecologist" });
            var hemat = context.Specializations
                .Add(new Specialization { Name = "Hematologist" });
            var hepat = context.Specializations
                .Add(new Specialization { Name = "Hepatologist" });
            var intern = context.Specializations
                .Add(new Specialization { Name = "Internist" });
            var nephrol = context.Specializations
                .Add(new Specialization { Name = "Nephrologist" });
            var neurol = context.Specializations
                .Add(new Specialization { Name = "Neurologist" });
            var onco = context.Specializations
                .Add(new Specialization { Name = "Oncologist" });
            var ophtal = context.Specializations
                .Add(new Specialization { Name = "Ophthalmologist" });
            var dent = context.Specializations
                .Add(new Specialization { Name = "Dentist" });
            var otol = context.Specializations
                .Add(new Specialization { Name = "Otolaryngologist" });
            var pathol = context.Specializations
                .Add(new Specialization { Name = "Pathologist" });
            var perint = context.Specializations
                .Add(new Specialization { Name = "Perinatologist" });
            var phys = context.Specializations
                .Add(new Specialization { Name = "Physiatrist" });
            var psy = context.Specializations
                .Add(new Specialization { Name = "Psychiatrist" });
            var pulm = context.Specializations
                .Add(new Specialization { Name = "Pulmonologist" });
            var radiol = context.Specializations
                .Add(new Specialization { Name = "Radiologist" });
            var rheum = context.Specializations
                .Add(new Specialization { Name = "Rheumatologist" });
            var urol = context.Specializations
                .Add(new Specialization { Name = "Urologist" });
            var diagnost = context.Specializations
                .Add(new Specialization { Name = "Diagnostician" });

            context.SaveChanges();

            //doctors
            var diagnostician = context.Doctors.Add(new Doctor
            {
                Name = "Gregory House"
            });
            diagnostician.Specializations.Add(diagnost);

            var internist = context.Doctors.Add(new Doctor
            {
                Name = "Meredith Grey"
            });
            internist.Specializations.Add(intern);

            var emergenist = context.Doctors.Add(new Doctor
            {
                Name = "Konrad Hawkins"
            });
            emergenist.Specializations.Add(emerg);

            var psychiastrist = context.Doctors.Add(new Doctor
            {
                Name = "Carla Espinosa"
            });
            psychiastrist.Specializations.Add(psy);

            var surgeon = context.Doctors.Add(new Doctor
            {
                Name = "Christopher Turk"
            });
            surgeon.Specializations.Add(surg);
            surgeon.Specializations.Add(cardiovascsurg);
            surgeon.Specializations.Add(neurosurg);

            var intsurgeon = context.Doctors.Add(new Doctor
            {
                Name = "Todd Quinlan"
            });
            intsurgeon.Specializations.Add(intern);
            intsurgeon.Specializations.Add(orthosurg);

            context.SaveChanges();

            //patients
            var patient1 = context.Patients.Add(new Patient
            {
                Name = "Kathryn Joosten",
                Birthday = new DateTime(1939, 12, 20),
                Status = PatientStatus.Sick,
                TaxCode = "P4-5005-4404-1111-0000"
            });
            patient1.Doctors.Add(intsurgeon);
            patient1.Doctors.Add(surgeon);

            var patient2 = context.Patients.Add(new Patient
            {
                Name = "Michael Robert McDonald",
                Birthday = new DateTime(1991, 01, 15),
                Status = PatientStatus.Healthy,
                TaxCode = "P4-5005-4404-1111-0010"
            });
            patient2.Doctors.Add(psychiastrist);

            var patient3 = context.Patients.Add(new Patient
            {
                Name = "Nicole Sullivan",
                Birthday = new DateTime(1970, 04, 21),
                Status = PatientStatus.Arrived,
                TaxCode = "P4-5005-4404-1111-0011"
            });
            patient3.Doctors.Add(diagnostician);
            patient3.Doctors.Add(emergenist);

            var patient4 = context.Patients.Add(new Patient
            {
                Name = "Maree Cheatham",
                Birthday = new DateTime(1942, 06, 02),
                Status = PatientStatus.Sick,
                TaxCode = "P4-5005-4404-1111-0100"
            });
            patient4.Doctors.Add(diagnostician);
            patient4.Doctors.Add(psychiastrist);

            var patient5 = context.Patients.Add(new Patient
            {
                Name = "Alan Ruck",
                Birthday = new DateTime(1956, 07, 01),
                Status = PatientStatus.Sick,
                TaxCode = "P4-5005-4404-1111-0101"
            });
            patient5.Doctors.Add(internist);

            context.SaveChanges();
        }
    }
}