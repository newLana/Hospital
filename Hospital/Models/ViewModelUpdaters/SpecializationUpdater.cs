using Hospital.DAL.Abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models.ViewModelUpdaters
{
    public class SpecializationUpdater
    {
        IDataBaseUnit db;

        public SpecializationUpdater(IDataBaseUnit db)
        {
            this.db = db;
        }

        public SpecializationViewModel FromSpecialization(Specialization specialization)
        {
            var viewModel = new SpecializationViewModel();
            if(specialization.Id != null)
            {
                viewModel.Id = specialization.Id;
            }
            viewModel.Name = specialization.Name;
            viewModel.Doctors = GetDoctorsMultiSelect(specialization.Doctors.Select(d => d.Id));
            return viewModel;
        }

        public MultiSelectList GetDoctorsMultiSelect(IEnumerable selectedVals)
        {
            return new MultiSelectList(db.Doctors.GetAll(), "Id", "Name", selectedVals);
        }

        public Specialization ToSpecialization(SpecializationViewModel viewModel)
        {
            Specialization specialization;
            if (viewModel != null)
            {
                specialization = db.Specializations.Get((int)viewModel.Id);
                specialization.Doctors.Clear();
            }
            else
            {
                specialization = new Specialization();
            }
            specialization.Name = viewModel.Name;
            if (viewModel.DocIds != null)
            {
                specialization.Doctors.AddRange(GetDoctors(viewModel));
            }
            return specialization;
        }

        private IEnumerable<Doctor> GetDoctors(SpecializationViewModel viewModel)
        {
            return db.Doctors.GetAll().Where(d =>
                   viewModel.DocIds.Contains((int)d.Id));
        }
    }
}