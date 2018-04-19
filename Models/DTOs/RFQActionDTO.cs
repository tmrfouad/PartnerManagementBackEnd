using System;
using System.Collections.Generic;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models.DTOs
{
    public class RFQActionDTO
    {
        public int Id { get; set; }
        public string ActionCode { get; set; }
        public DateTime ActionTime { get; set; }
        public ActionType ActionType { get; set; }
        public string Comments { get; set; }
        public string UniversalIP { get; set; }
        public DateTime SubmissionTime { get; set; }

        // Navigation Properties
        public int RFQId { get; set; }
        public int RepresentativeId { get; set; }
        public RepresentativeDTO Representative { get; set; }
        public ICollection<RFQActionAttDTO> RFQActionAtts { get; set; }
    }
}