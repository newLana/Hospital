using Hospital.DAL.Abstracts;
using Hospital.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        IDataBaseUnit db;

        public DoctorController(IDataBaseUnit dbUnit)
        {
            db = dbUnit;
        }

        [HttpGet]
        public ActionResult Index(string searchKey)
        {
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                return View("Index", db.Doctors.GetAll()
                    .Where(d => d.Name.Contains(searchKey)));
            }
            return View(db.Doctors.GetAll());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Get((int)id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new DoctorViewModel();
            viewModel.Specializations = new MultiSelectList(db.Specializations
                                    .GetAll().OrderBy(s => s.Name), "Id", "Name");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Name,SpecIds")]
                                    DoctorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = new Doctor
                {
                    Name = viewModel.Name
                };
                foreach (Specialization item in GetSpecs(viewModel.SpecIds))
                {
                    doctor.Specializations.Add(item);
                }                
                db.Doctors.Create(doctor);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Get((int)id);
            if(doctor == null)
            {
                return HttpNotFound();
            }
            var viewModel = new DoctorViewModel
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specializations = new MultiSelectList(db.Specializations.GetAll().OrderBy(s => s.Name),
                                    "Id", "Name", doctor.Specializations.Select(s => s.Id)),
                Patients = new MultiSelectList(db.Patients.GetAll(), 
                                    "Id", "Name", doctor.Patients.Select(p => p.Id))
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SpecIds,PatientIds")]
                                                    DoctorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = db.Doctors.Get(viewModel.Id);
                doctor.Name = viewModel.Name;
                doctor.Specializations.Clear();
                foreach (Specialization item in GetSpecs(viewModel.SpecIds))
                {
                    doctor.Specializations.Add(item);
                }
                doctor.Patients.Clear();
                foreach (Patient item in GetPatients(viewModel.PatientIds))
                {
                    doctor.Patients.Add(item);
                }
                db.Doctors.Update(doctor);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        private IEnumerable<Specialization> GetSpecs(IEnumerable<int> ids)
        {
            if (ids != null)
            {
                return db.Specializations.GetAll().Where(s => ids.Contains(s.Id));
            }
            return Enumerable.Empty<Specialization>();
        }

        private IEnumerable<Patient> GetPatients(IEnumerable<int> ids)
        {
            if (ids != null)
            {
                return db.Patients.GetAll().Where(s => ids.Contains(s.Id));
            }
            return Enumerable.Empty<Patient>();
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Get((int)id);
            if(doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            db.Doctors.Delete(id);            
            return RedirectToAction("Index");
        }        
    }
}