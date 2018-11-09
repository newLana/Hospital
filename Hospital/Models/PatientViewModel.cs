using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field Name cannot be empty.")]
        [MaxLength(30, ErrorMessage = "To long name.")]
        [RegularExpression("([A-Za-z]+\\s?)+", ErrorMessage =
            "Field Name can contains only letters or whitespeces.")]
        public string Name { get; set; }
        
        public PatientStatus Status { get; set; }

        [Remote("CheckBirth", "Patient", ErrorMessage =
            "Not valid date of birth.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]        
        public DateTime Birthday { get; set; }

        #region
        //[Remote("UniqueTaxCheck", "Patient", ErrorMessage =
        //    "TaxCode with same value already exists in DB. Are you maked mistake?")]
        #endregion
        [Required(ErrorMessage = "Field TaxCode cannot be empty.")]
        public string TaxCode { get; set; }

        public MultiSelectList Doctors { get; set; }

        public List<int> DocIds { get; set; }
    }
}