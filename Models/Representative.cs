

using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Models
{
    public class Representative 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string PersonalPhone { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public bool? Continuous { get; set; }

        public DateTime Created { get; set; }
        public string UniversalIP { get; set; }

        public ICollection<RFQAction> RFQActions { get; set; }
    }
}