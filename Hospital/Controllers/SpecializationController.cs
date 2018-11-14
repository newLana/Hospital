using Hospital.DAL.Abstracts;
using Hospital.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System;
using Hospital.Models.ViewModelUpdaters;

namespace Hospital.Controllers
{
    public class SpecializationController : Controller
    {
        IDataBaseUnit db;
        SpecializationUpdater updater;

        public SpecializationController(IDataBaseUnit db)
        {
            this.db = db;
            updater = new SpecializationUpdater(db);
        }

        // GET: Specialization
        public ActionResult Index(string searchKey)
        {
            if(!string.IsNullOrEmpty(searchKey))
            {
                return View(db.Specializations.GetAll().
                    Where(s => s.Name.CaseInsensitiveContains(searchKey)));
            }
            return View(db.Specializations.GetAll().OrderBy(s => s.Name));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")]Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                db.Specializations.Create(specialization);
                return RedirectToAction("Index");
            }
            return View(specialization);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var spec = db.Specializations.Get((int)id);
            if(spec == null)
            {
                return HttpNotFound();
            }
            return View(spec);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var spec = db.Specializations.Get((int)id);
            if (spec == null)
            {
                return HttpNotFound();
            }
            var viewModel = updater.FromSpecialization(spec);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpecializationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var spec = updater.ToSpecialization(viewModel);
                db.Specializations.Update(spec);
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
            var spec = db.Specializations.Get((int)id);
            if (spec == null)
            {
                return HttpNotFound();
            }
            return View(spec);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            db.Specializations.Delete(id);
            return RedirectToAction("Index");
        }

        public JsonResult UniqueName(string name, int? id)
        {
            var specialization = db.Specializations.GetAll()
                .FirstOrDefault(s => s.Name == name);
            bool isUnique;
            if (id == null)
            {
                isUnique = specialization == null;
            }
            else
            {
                isUnique = specialization == null || specialization.Id == id;
            }
            return Json(isUnique, JsonRequestBehavior.AllowGet);
        }
    }
}