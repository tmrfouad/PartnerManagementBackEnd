using System;

namespace acscustomersgatebackend.Models
{
    public class RFQActionAtt
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public int RFQActionId { get; set; }

        public RFQAction RFQAction { get; set; }

        public string Value { get; set; }
    }
}