using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class Specialization
    {
        public int? Id { get; set; }
        
        [Required]
        [Remote("UniqueName", "Specialization", AdditionalFields = "Id", 
            ErrorMessage = "Specialization with same name already exists.")]
        public string Name { get; set; }

        public virtual List<Doctor> Doctors { get; set; } = 
            new List<Doctor>();
    }
}