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
            CreateMap<PartnerDto, Partner>()
            .ReverseMap();
            CreateMap<SubscriptionDto, Subscription>()
            .ReverseMap()
                .ForMember(tgt => tgt.ProductName, opt => opt.MapFrom(src => src.Product.EnglishName))
                .ForMember(tgt => tgt.ProductEditionName, opt => opt.MapFrom(src => src.ProductEdition.EnglishName));
            CreateMap<InvoiceDto, Invoice>()
            .ReverseMap();
        }
    }
}