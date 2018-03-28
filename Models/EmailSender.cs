using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Models
{
    public class EmailSender
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime Created { get; set; }
        public string UniversalIP { get; set; }
    }
}