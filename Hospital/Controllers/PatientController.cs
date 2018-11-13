using Hospital.DAL.Abstracts;
using Hospital.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class PatientController : Controller
    {
        IDataBaseUnit db;

        public PatientController(IDataBaseUnit dbUnit)
        {
            db = dbUnit;
        }

        [HttpGet]
        public ActionResult Index(string searchKey)
        {
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                return View("Index", db.Patients.GetAll()
                    .Where(d => d.Name.Contains(searchKey)));
            }
            return View(db.Patients.GetAll());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Get((int)id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new PatientViewModel();
            viewModel.Birthday = DateTime.Now;
            viewModel.Doctors = new MultiSelectList(db.Doctors.GetAll(), "Id", "Name");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Name,Status,Birthday,TaxCode,DocIds")]
                                    PatientViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = new Patient
                {
                    Name = viewModel.Name,
                    Status = viewModel.Status,
                    Birthday = viewModel.Birthday,
                    TaxCode = viewModel.TaxCode
                };
                patient.Doctors.AddRange(db.Doctors.GetAll()
                                .Where(d => viewModel.DocIds.Contains(d.Id)));
                db.Patients.Create(patient);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Get((int)id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var viewModel = new PatientViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                Status = patient.Status,
                Birthday = patient.Birthday,
                TaxCode = patient.TaxCode,
                Doctors = new MultiSelectList(db.Doctors.GetAll(),
                                    "Id", "Name", patient.Doctors.Select(d => d.Id))
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Status,Birthday,TaxCode,DocIds")]
                                                    PatientViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = db.Patients.Get((int)viewModel.Id);
                patient.Name = viewModel.Name;
                patient.Status = viewModel.Status;
                patient.Birthday = viewModel.Birthday;
                patient.TaxCode = viewModel.TaxCode;
                patient.Doctors.Clear();
                patient.Doctors.AddRange(db.Doctors.GetAll().Where(d => 
                            viewModel.DocIds.Contains(d.Id)));
                db.Patients.Update(patient);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Get((int)id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            db.Patients.Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult CheckBirth(DateTime birthday)
        {
            return Json(!(DateTime.Now.Date < birthday.Date), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UniqueTaxCheck(string taxcode, int? id)
        {
            bool result;
            var patient = db.Patients.GetAll().FirstOrDefault(p => p.TaxCode == taxcode);
            if (id == null)
            {
                result = (patient != null);
            }
            else
            {
                result = (patient != null) && (patient.Id != id);
            }
            return Json(!result, JsonRequestBehavior.AllowGet);
        }     
    }
}