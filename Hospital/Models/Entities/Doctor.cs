using System.Collections.Generic;

namespace Hospital.Models
{
    public class Doctor
    {
        public int? Id { get; set; }

        public string Name { get; set; }
        
        public virtual List<Specialization> Specializations { get; set; } = 
            new List<Specialization>();
        
        public virtual List<Patient> Patients { get; set; } = 
            new List<Patient>();

        public string AccountId { get; set; }
    }
}