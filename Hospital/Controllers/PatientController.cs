﻿using Hospital.DAL.Repository.EfRepository;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class PatientController : Controller
    {
        HospitalDB db = new HospitalDB();

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
                foreach (var item in GetDocs(viewModel.DocIds))
                {
                    patient.Doctors.Add(item);
                }
                db.Patients.Create(patient);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        private IEnumerable<Doctor> GetDocs(List<int> docIds)
        {
            if (docIds != null)
            {
                return db.Doctors.GetAll().Where(d => docIds.Contains(d.Id));
            }
            return Enumerable.Empty<Doctor>();
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
        public ActionResult Edit([Bind(Include = "Id,Name,Status,Birthday,TaxCode,DocIds")]
                                                    PatientViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = db.Patients.Get(viewModel.Id);
                patient.Name = viewModel.Name;
                patient.Status = viewModel.Status;
                patient.Birthday = viewModel.Birthday;
                patient.TaxCode = viewModel.TaxCode;
                patient.Doctors.Clear();
                patient.Doctors.AddRange(GetDocs(viewModel.DocIds));
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
        public ActionResult Delete(int id)
        {
            db.Patients.Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult CheckBirth(DateTime birthday)
        {
            return Json(!(DateTime.Now.Date < birthday.Date), JsonRequestBehavior.AllowGet);
        }

        #region
        //public JsonResult UniqueTaxCheck(string taxcode)
        //{
        //    var result = db.Patients.GetAll().Any(p => p.TaxCode == taxcode);
        //    return Json(!result, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}