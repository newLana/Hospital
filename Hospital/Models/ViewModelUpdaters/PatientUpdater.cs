using Hospital.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models.ViewModelUpdaters
{
    public class PatientUpdater
    {
        IDataBaseUnit db;

        public PatientUpdater(IDataBaseUnit db)
        {
            this.db = db;
        }
        
        public Patient ToPatient(PatientViewModel viewModel)
        {
            Patient patient;
            if (viewModel.Id != null)
            {
                patient = db.Patients.Get((int)viewModel.Id);
            }
            else
            {
                patient = new Patient();
            }
            patient.Name = viewModel.Name;
            patient.Status = viewModel.Status;
            patient.Birthday = viewModel.Birthday;
            patient.TaxCode = viewModel.TaxCode;
            patient.Doctors.Clear();
            if (viewModel.DocIds != null)
            {
                patient.Doctors.AddRange(DoctorsForPatient(viewModel));
            }
            return patient;
        }

        private IEnumerable<Doctor> DoctorsForPatient(PatientViewModel viewModel)
        {
            return db.Doctors.GetAll().Where(d =>
                     viewModel.DocIds.Contains((int)d.Id));
        }

        public PatientViewModel FromPatient(Patient patient)
        {
            var viewModel = new PatientViewModel();
            if (patient.Id != null)
            {
                viewModel.Id = patient.Id;
            }
            viewModel.Name = patient.Name;
            viewModel.Status = patient.Status;
            viewModel.Birthday = patient.Birthday;
            viewModel.TaxCode = patient.TaxCode;
            viewModel.Doctors = DoctorsToMultiselect(patient.Doctors.Select(d => d.Id));
            return viewModel;
        }

        public MultiSelectList DoctorsToMultiselect(IEnumerable selectedVals)
        {
            return new MultiSelectList(db.Doctors.GetAll(),
                        "Id", "Name", selectedVals);
        }
    }
}