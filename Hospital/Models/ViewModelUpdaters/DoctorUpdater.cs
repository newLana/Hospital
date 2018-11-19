using Hospital.DAL.Abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models.Entities
{
    public class DoctorUpdater
    {
        IDataBaseUnit db;

        public DoctorUpdater(IDataBaseUnit db)
        {
            this.db = db;
        }

        public Doctor ToDoctor(DoctorEditModel viewModel)
        {
            Doctor doctor;
            if (viewModel.Id == null)
            {
                doctor = new Doctor();
            }
            else
            {
                doctor = db.Doctors.Get((int)viewModel.Id);
                doctor.Specializations.Clear();
                doctor.Patients.Clear();
            }
            doctor.Name = viewModel.Name;
            if (viewModel.SpecIds != null)
            {
                doctor.Specializations.AddRange(SpecializationsForDoctor(viewModel));
            }
            if (viewModel.PatientIds != null)
            {
                doctor.Patients.AddRange(PatientsForDoctor(viewModel));
            }
            doctor.AccountId = viewModel.AccountId;
            return doctor;
        }

        private IEnumerable<Specialization> SpecializationsForDoctor(DoctorEditModel viewModel)
        {
            return db.Specializations.GetAll().Where(s =>
                   viewModel.SpecIds.Contains((int)s.Id));
        }

        private IEnumerable<Patient> PatientsForDoctor(DoctorEditModel viewModel)
        {
            return db.Patients.GetAll().Where(p =>
                                viewModel.PatientIds.Contains((int)p.Id));
        }

        public DoctorEditModel FromDoctor(Doctor doctor)
        {
            DoctorEditModel viewModel = new DoctorEditModel();

            if (doctor.Id != null)
            {
                viewModel.Id = doctor.Id;
            }
            viewModel.Name = doctor.Name;

            viewModel.Specializations = SpecToMultiselect(doctor
                    .Specializations.Select(s => s.Id));

            viewModel.Patients = PatientsToMultiselect(doctor.Patients.Select(p => p.Id));

            viewModel.AccountId = doctor.AccountId;
            return viewModel;
        }

        public MultiSelectList SpecToMultiselect(IEnumerable selectedVals)
        {
            return new MultiSelectList(db.Specializations.GetAll()
                .OrderBy(s => s.Name), "Id", "Name", selectedVals);
        }

        public MultiSelectList PatientsToMultiselect(IEnumerable selectedVals)
        {
            return new MultiSelectList(db.Patients.GetAll()
                .OrderBy(s => s.Name), "Id", "Name", selectedVals);
        }
    }
}