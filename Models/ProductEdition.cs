using System;
using System.Collections.Generic;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models
{
    public class ProductEdition
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public int ProductId { get; set; }

        public DateTime Created { get; set; }
        public string UniversalIP { get; set; }

        // Navigation Properties
        public Product Product { get; set; }
        public ICollection<RFQ> RFQs { get; set; }
        
    }
}