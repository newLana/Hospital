using Hospital.DAL.Abstracts;
using Hospital.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IDataBaseUnit db;

        public HomeController(IDataBaseUnit db)
        {
            this.db = db;
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // GET: Home
        public ActionResult Index()
        {
            if (User.IsInRole("patient"))
            {
                var patient = db.Patients.GetAll().FirstOrDefault(p => p.AccountId == User.Identity.GetUserId());
                if (patient != null)
                {                    
                    return RedirectToAction("Details", "Patient", new { id = patient.Id });
                }
            }            
            return View();
        }
    }
}