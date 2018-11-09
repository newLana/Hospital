using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual List<Specialization> Specializations { get; set; } = 
            new List<Specialization>();
        
        public virtual ICollection<Patient> Patients { get; set; } = 
            new List<Patient>();
    }
}