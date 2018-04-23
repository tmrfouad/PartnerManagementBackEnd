using System;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models.DTOs
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductEditionId { get; set; }
        public string ProductEditionName { get; set; }
        public int PartnerId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public SubscriptionStatus Status { get; set; }
        public string UniversalIP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}