using System;

namespace PartnerManagement.Models
{
    public class SubscriptionUser
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string UniversalIP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        // Navigation Properties
        public Subscription Subscription { get; set; }
    }
}