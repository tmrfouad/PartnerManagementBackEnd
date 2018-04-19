using System;

namespace PartnerManagement.Models.DTOs
{
    public class ProductEditionDTO
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public int ProductId { get; set; }

        public DateTime Created { get; set; }
        public string UniversalIP { get; set; }
    }
}