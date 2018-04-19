using System;

namespace PartnerManagement.Models.DTOs
{
    public class EmailSenderDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime Created { get; set; }
        public string UniversalIP { get; set; }
    }
}