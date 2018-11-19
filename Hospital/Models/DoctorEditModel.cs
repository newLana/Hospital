using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class DoctorEditModel
    {
        public int? Id { get; set; }

        [Required]
        [RegularExpression("([A-Za-z]+\\s?)+",
            ErrorMessage = "Field Name can contains only letters or whitespeces.")]
        [MaxLength(30)]
        public string Name { get; set; }

        public List<int> SpecIds { get; set; }

        public MultiSelectList Specializations { get; set; }

        public List<int> PatientIds { get; set; }

        public MultiSelectList Patients { get; set; }

        public string AccountId { get; set; }

        [Required]
        public string Email { get; set; }
    }
}