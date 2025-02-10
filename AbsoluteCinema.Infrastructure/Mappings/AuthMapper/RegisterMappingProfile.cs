using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Infrastructure.Identity.Data;
using AutoMapper;

namespace AbsoluteCinema.Infrastructure.Mappings.AuthMapper;

public class RegisterMappingProfile : Profile
{
    public RegisterMappingProfile()
    {
        CreateMap<RegisterDto, ApplicationUser>()
            // Username берем часть от email 
            .ForMember(dest => dest.UserName, opt => 
                opt.MapFrom(src => src.Email.Substring(0, src.Email.IndexOf('@'))))
                
            .ForMember(dest => dest.Email,        opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName,    opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,     opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.BirthDate,    opt => opt.MapFrom(src => src.BirthDate))
                
            // Игнорируем ВСЕ свойства Identity
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Tickets, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore());
    }
}