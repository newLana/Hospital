using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Hospital.Models
{
    public class Patient
    {
        public int? Id { get; set; }

        public string Name { get; set; }
        
        public PatientStatus Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public string TaxCode { get; set; }

        public virtual List<Doctor> Doctors { get; set; } =
            new List<Doctor>();

        public string AccountId { get; set; }
    }
}