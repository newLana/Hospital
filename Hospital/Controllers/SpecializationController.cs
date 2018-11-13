using Hospital.DAL.Abstracts;
using Hospital.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class SpecializationController : Controller
    {
        IDataBaseUnit db;

        public SpecializationController(IDataBaseUnit db)
        {
            this.db = db;
        }

        // GET: Specialization
        public ActionResult Index(string searchKey)
        {
            if(!string.IsNullOrEmpty(searchKey))
            {
                return View(db.Specializations.GetAll().Where(s => s.Name.Contains(searchKey)));
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
        public ActionResult Create(Specialization specialization)
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
            var viewModel = new SpecializationViewModel()
            {
                Id = spec.Id,
                Name = spec.Name
            };
            viewModel.Doctors = new MultiSelectList(db.Doctors.GetAll(), "Id", "Name", spec.Doctors.Select(d => d.Id));
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpecializationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var spec = db.Specializations.Get(viewModel.Id);
                spec.Id = viewModel.Id;
                spec.Name = viewModel.Name;
                spec.Doctors.Clear();
                spec.Doctors.AddRange(db.Doctors.GetAll().Where(d => 
                                    viewModel.DocIds.Contains(d.Id)));

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

        //public JsonResult UniqueName(string name)
        //{
        //    var isUnique = !db.Specializations.GetAll().Any(s => s.Name == name);
        //    return Json(isUnique, JsonRequestBehavior.AllowGet);
        //}
    }
}