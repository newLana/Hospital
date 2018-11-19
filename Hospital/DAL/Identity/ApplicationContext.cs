﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Account
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("IdentiyDb")
        {}

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}