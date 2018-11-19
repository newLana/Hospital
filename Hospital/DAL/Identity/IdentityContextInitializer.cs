using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Hospital.Models.Account
{
    public class IdentityContextInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };

            var role2 = new IdentityRole { Name = "doctor" };

            var role3 = new IdentityRole { Name = "patient" };

            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            var admin = new ApplicationUser { Email = "test@admin.com", UserName = "test@admin.com" };
            string password = "123456";
            var result = userManager.Create(admin, password);
            
            if(result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
            }
            base.Seed(context);
        }
    }
}