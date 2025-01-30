using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Infrastructure.Identity.Data;
using AutoMapper;

namespace AbsoluteCinema.Application.Mappings.AuthMapping;

public class LoginMappingProfile : Profile
{
    public LoginMappingProfile()
    {
        CreateMap<LoginDto, ApplicationUser>()
            // Username берем часть от email 
            .ForMember(dest => dest.UserName, opt => 
                opt.MapFrom(src => src.Email.Substring(0, src.Email.IndexOf('@'))))
                
            .ForMember(dest => dest.Email,        opt => opt.MapFrom(src => src.Email))
                
            // Игнорируем поля от Identity (ставятся автоматически)
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.Id,           opt => opt.Ignore())
            .ForMember(dest => dest.FirstName,    opt => opt.Ignore())
            .ForMember(dest => dest.LastName,     opt => opt.Ignore())
            .ForMember(dest => dest.BirthDate,    opt => opt.Ignore());
    }
    
}