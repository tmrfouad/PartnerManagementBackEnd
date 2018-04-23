using System;

namespace PartnerManagement.Models.DTOs
{
    public class PartnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string UniversalIP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}