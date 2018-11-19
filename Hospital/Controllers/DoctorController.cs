using Hospital.DAL.Abstracts;
using Hospital.Models;
using Hospital.Models.Account;
using Hospital.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    [Authorize(Roles = "admin,doctor")]
    public class DoctorController : Controller
    {
        IDataBaseUnit db;

        DoctorUpdater updater;

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

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
        public ActionResult Create(DoctorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var registerModel = new RegisterViewModel
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password
                };
                if (Register(registerModel))
                {
                    var user = UserManager.FindByEmail(registerModel.Email);
                    if (user != null)
                    {
                        UserManager.AddToRole(user.Id, "doctor");
                        viewModel.AccountId = user.Id;
                        Doctor doctor = updater.ToDoctor(viewModel);
                        db.Doctors.Create(doctor);
                        user.EntityId = (int)doctor.Id;
                        return RedirectToAction("Index", "Doctor");
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
            viewModel.Email = UserManager.FindById(doctor.AccountId).Email;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id,AccountId,Email,Name,SpecIds,PatientIds")]
        DoctorEditModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = updater.ToDoctor(viewModel);
                db.Doctors.Update(doctor);
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
            var doctor = db.Doctors.Get(id); 
            if (doctor.AccountId != User.Identity.GetUserId())
            {
                var user = UserManager.FindById(doctor.AccountId);
                if (user != null && UserManager.Delete(user).Succeeded)
                {
                    db.Doctors.Delete(id);
                }
                else
                {
                    return new HttpStatusCodeResult(500);
                }
            }
            return RedirectToAction("Index");
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