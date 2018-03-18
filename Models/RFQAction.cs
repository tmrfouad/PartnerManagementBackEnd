using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Models
{
    public class RFQAction
    {
        public int Id { get; set; }
        public string ActionCode { get; set; }
        public DateTime ActionTime { get; set; }
        public Enumerations.ActionType ActionType { get; set; }
        public string Comments { get; set; }
        public string UniversalIP { get; set; }
        public DateTime SubmissionTime { get; set; }

        // Navigation Properties
        public int RFQId { get; set; }
        public RFQ RFQ { get; set; }
        public int RepresentativeId { get; set; }
        public Representative Representative { get; set; }
        public ICollection<RFQActionAtt> RFQActionAtts { get; set; }
    }
}