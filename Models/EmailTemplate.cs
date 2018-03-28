using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Models
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string HtmlTemplate { get; set; }

        public DateTime Created { get; set; }
        public string UniversalIP { get; set; }
    }
}