using System.Collections.Generic;

namespace Hospital.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Doctor> Doctors { get; set; } = 
            new List<Doctor>();
    }
}