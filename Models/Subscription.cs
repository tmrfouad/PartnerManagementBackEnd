using System;
using System.Collections.Generic;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductEditionId { get; set; }
        public int PartnerId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public SubscriptionStatus Status { get; set; }
        public string UniversalIP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        // Navigation Properties
        public Product Product { get; set; }
        public ProductEdition ProductEdition { get; set; }
        public Partner Partner { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}