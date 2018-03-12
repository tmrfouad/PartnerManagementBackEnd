using System;

namespace acscustomersgatebackend.Models
{
    public class RFQAction
    {
        public int Id { get; set; }
        public string ActionCode { get; set; }
        public DateTime ActionTime { get; set; }
        public Enumerations.ActionType ActionType { get; set; }
        public string CompanyRepresentative { get; set; }
        public string Comments { get; set; }
        public int RFQId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public string UniversalIP { get; set; }

        // Navigation Properties
        public RFQ RFQ { get; set; }
    }
}