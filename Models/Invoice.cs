using System;
using System.Collections.Generic;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public InvoiceStatus Status { get; set; }
        public bool Paid { get; set; }
        public string UniversalIP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        // Navigation Properties
        public Subscription Subscription { get; set; }
        public ICollection<InvoiceActivity> InvoiceActivities { get; set; }
    }
}