using System;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models.DTOs
{
    public class InvoiceActivityDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public InvActivity Activity { get; set; }
        public DateTime Date { get; set; }
        public string UniversalIP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}