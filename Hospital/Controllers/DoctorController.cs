using Hospital.DAL.Abstracts;
using Hospital.Models;
using Hospital.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        IDataBaseUnit db;
        DoctorUpdater updater;

        public DoctorController(IDataBaseUnit dbUnit)
        {
            db = dbUnit;
            updater = new DoctorUpdater(db);
        }

        [HttpGet]
        public ActionResult Index(string searchKey)
        {
            if(string.IsNullOrEmpty(searchKey))
            {
                return View(db.Doctors.GetAll());
            }
            return View(db.Doctors.GetAll()
                .Where(d => d.Name.CaseInsensitiveContains(searchKey)));
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
            viewModel.Specializations = updater.SpecToMultiselect(null);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Name,SpecIds")]
                                    DoctorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = updater.ToDoctor(viewModel);
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

            var viewModel = updater.FromDoctor(doctor);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SpecIds,PatientIds")]
                                                    DoctorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = updater.ToDoctor(viewModel);

                db.Doctors.Update(doctor);

                return RedirectToAction("Index");
            }
            return View(viewModel);
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            db.Doctors.Delete(id);                 
            return RedirectToAction("Index");
        }        
    }
}