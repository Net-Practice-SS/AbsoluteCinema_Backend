using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Infrastructure.Identity.Data;
using AutoMapper;

namespace AbsoluteCinema.Application.Mappings.AuthMapping;

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
                
            // Игнорируем поля от Identity (ставятся автоматически)
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Id,           opt => opt.Ignore());
    }
}