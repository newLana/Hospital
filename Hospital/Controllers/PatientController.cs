using Hospital.DAL.Abstracts;
using Hospital.Models;
using Hospital.Models.Account;
using Hospital.Models.ViewModelUpdaters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{    
    public class PatientController : Controller
    {
        IDataBaseUnit db;

        PatientUpdater updater;

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public PatientController(IDataBaseUnit dbUnit)
        {
            db = dbUnit;
            updater = new PatientUpdater(db);
        }

        [HttpGet]
        [Authorize(Roles = "admin,doctor")]
        public ActionResult Index(string searchKey)
        {
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                return View("Index", db.Patients.GetAll()
                    .Where(d => d.Name.CaseInsensitiveContains(searchKey)));
            }
            return View(db.Patients.GetAll());
        }

        [HttpGet]
        [Authorize(Roles = "admin,doctor,patient")]
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
        [Authorize(Roles = "admin,doctor")]
        public ActionResult Create()
        {
            var viewModel = new PatientViewModel();
            viewModel.Birthday = DateTime.Now;            
            viewModel.Doctors = updater.DoctorsToMultiselect(null);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,doctor")]
        public ActionResult Create(PatientViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var registerModel = new RegisterViewModel
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password
                };                
                if(Register(registerModel))
                {
                    var user = UserManager.FindByEmail(registerModel.Email);
                    if (user != null)
                    {
                        UserManager.AddToRole(user.Id, "patient");
                        viewModel.AccountId = user.Id;
                        Patient patient = updater.ToPatient(viewModel);
                        patient = db.Patients.Create(patient);

                        user.EntityId = (int)patient.Id;
                        return RedirectToAction("Index", "Patient");
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(500);
                }                 
            }
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "admin,doctor")]
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
            var viewModel = updater.FromPatient(patient);
            viewModel.Email = UserManager.FindById(patient.AccountId).Email;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,doctor")]
        public ActionResult Edit(PatientEditModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = updater.ToPatient(viewModel);
                db.Patients.Update(patient);
                var user = UserManager.FindById(viewModel.AccountId);
                user.Email = viewModel.Email;
                user.UserName = viewModel.Email;
                if (UserManager.Update(user).Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return new HttpStatusCodeResult(500);
            }
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "admin,doctor")]
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
        [Authorize(Roles = "admin,doctor")]
        public ActionResult Delete(int id)
        {
            var patient = db.Patients.Get(id);
            if (patient.AccountId != User.Identity.GetUserId())
            {
                var user = UserManager.FindById(patient.AccountId);
                if (user != null && UserManager.Delete(user).Succeeded)
                {
                    db.Patients.Delete(id);
                }
                else
                {
                    return new HttpStatusCodeResult(500);
                }
            }
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

        private bool Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                IdentityResult result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    result = UserManager.AddToRole(user.Id, "patient");
                    return result.Succeeded;
                }
            }
            return false;
        }
    }
}