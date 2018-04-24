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
                .ForMember(tgt => tgt.ProductEnglishName, opt => opt.MapFrom(src => src.Product.EnglishName))
                .ForMember(tgt => tgt.ProductArabicName, opt => opt.MapFrom(src => src.Product.ArabicName))
                .ForMember(tgt => tgt.ProductEditionEnglishName, opt => opt.MapFrom(src => src.ProductEdition.EnglishName))
                .ForMember(tgt => tgt.ProductEditionArabicName, opt => opt.MapFrom(src => src.ProductEdition.ArabicName));
            CreateMap<InvoiceDto, Invoice>()
            .ReverseMap();
            CreateMap<SubscriptionUserDto, SubscriptionUser>()
            .ReverseMap();
            CreateMap<InvoiceActivityDto, InvoiceActivity>()
            .ReverseMap();
        }
    }
}