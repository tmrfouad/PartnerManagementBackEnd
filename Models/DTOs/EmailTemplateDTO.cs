using System;

namespace PartnerManagement.Models.DTOs
{
    public class EmailTemplateDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string HtmlTemplate { get; set; }

        public DateTime Created { get; set; }
        public string UniversalIP { get; set; }
    }
}