﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class SpecializationViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Int32> DocIds { get; set; }

        public MultiSelectList Doctors { get; set; }
    }
}