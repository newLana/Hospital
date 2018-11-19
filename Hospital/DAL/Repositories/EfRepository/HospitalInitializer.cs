using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital.DAL.Repositories.EfRepository
{
    public class HospitalInitializer : CreateDatabaseIfNotExists<HospitalContext>
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
        }
    }
}