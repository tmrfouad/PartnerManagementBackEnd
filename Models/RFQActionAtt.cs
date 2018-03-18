using System;

namespace acscustomersgatebackend.Models
{
    public class RFQActionAtt
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public int RFQActionId { get; set; }

        public RFQAction RFQAction { get; set; }
    }
}