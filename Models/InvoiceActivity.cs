using System;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models
{
    public class InvoiceActivity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public InvActivity Activity { get; set; }
        public DateTime Date { get; set; }
        public string UniversalIP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        // Navigation Properties
        public Invoice Invoice { get; set; }
    }
}