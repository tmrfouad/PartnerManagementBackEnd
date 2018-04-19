using System;
using System.Collections.Generic;

namespace PartnerManagement.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }

        public DateTime Created { get; set; }
        public string UniversalIP { get; set; }

        // Navigation Properties
        public ICollection<ProductEditionDTO> ProductEditions { get; set; }
    }
}