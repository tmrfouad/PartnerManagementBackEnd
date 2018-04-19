using AutoMapper;
using PartnerManagement.Models.DTOs;

namespace PartnerManagement.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmailSenderDTO, EmailSender>()
            .ReverseMap();
            CreateMap<EmailTemplateDTO, EmailTemplate>()
            .ReverseMap();
            CreateMap<ProductDTO, Product>()
            .ReverseMap();
            CreateMap<ProductEditionDTO, ProductEdition>()
            .ReverseMap();
            CreateMap<RepresentativeDTO, Representative>()
            .ReverseMap();
            CreateMap<RFQActionAttDTO, RFQActionAtt>()
            .ReverseMap();
            CreateMap<RFQActionDTO, RFQAction>()
            .ReverseMap();
            CreateMap<RFQDTO, RFQ>()
            .ReverseMap();
        }
    }
}