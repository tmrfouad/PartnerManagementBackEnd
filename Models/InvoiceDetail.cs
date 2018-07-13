using System;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string UniversalIP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        // Navigation Properties
        public Invoice Invoice { get; set; }
    }
}