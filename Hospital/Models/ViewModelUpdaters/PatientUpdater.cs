using Hospital.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Models.Account;
using Microsoft.AspNet.Identity.Owin;

namespace Hospital.Models.ViewModelUpdaters
{
    public class PatientUpdater
    {
        IDataBaseUnit db;        

        public PatientUpdater(IDataBaseUnit db)
        {
            this.db = db;
        }
        
        public Patient ToPatient(PatientEditModel viewModel)
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
            if (viewModel.DocIds != null)
            {
                patient.Doctors.Clear();
                patient.Doctors.AddRange(DoctorsForPatient(viewModel));
            }
            patient.AccountId = viewModel.AccountId;
            return patient;
        }

        private IEnumerable<Doctor> DoctorsForPatient(PatientEditModel viewModel)
        {
            return db.Doctors.GetAll().Where(d =>
                     viewModel.DocIds.Contains((int)d.Id));
        }

        public PatientEditModel FromPatient(Patient patient)
        {
            var viewModel = new PatientEditModel();
            if (patient.Id != null)
            {
                viewModel.Id = patient.Id;
            }
            viewModel.Name = patient.Name;
            viewModel.Status = patient.Status;
            viewModel.Birthday = patient.Birthday;
            viewModel.TaxCode = patient.TaxCode;
            viewModel.Doctors = DoctorsToMultiselect(patient.Doctors.Select(d => d.Id));
            viewModel.AccountId = patient.AccountId;
            return viewModel;
        }

        public MultiSelectList DoctorsToMultiselect(IEnumerable selectedVals)
        {
            return new MultiSelectList(db.Doctors.GetAll(),
                        "Id", "Name", selectedVals);
        }
    }
}