using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public virtual List<Doctor> Doctors { get; set; } = 
            new List<Doctor>();
    }
}