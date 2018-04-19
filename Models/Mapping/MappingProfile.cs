using AutoMapper;
using PartnerManagement.Models.DTOs;

namespace PartnerManagement.Models.Mapping
{
    public class MappingProfile : Profile
    {
        protected MappingProfile()
        {
            CreateMap<EmailSender, EmailSenderDTO>();
            CreateMap<EmailTemplate, EmailTemplateDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductEdition, ProductEditionDTO>();
            CreateMap<Representative, RepresentativeDTO>();
            CreateMap<RFQActionAtt, RFQActionAttDTO>();
            CreateMap<RFQAction, RFQActionDTO>();
            CreateMap<RFQ, RFQDTO>();
        }
    }
}