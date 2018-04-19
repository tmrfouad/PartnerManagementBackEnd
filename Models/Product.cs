using System;
using System.Collections.Generic;

namespace PartnerManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }

        public DateTime Created { get; set; }
        public string UniversalIP { get; set; }

        // Navigation Properties
        public ICollection<ProductEdition> ProductEditions { get; set; }
        public ICollection<RFQ> RFQs { get; set; }
    }
}