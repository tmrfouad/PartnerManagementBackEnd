using System;
using System.Collections.Generic;

namespace PartnerManagement.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string UniversalIP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        // Navigation Properties
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}