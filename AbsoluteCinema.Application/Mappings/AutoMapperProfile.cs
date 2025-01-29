using AutoMapper;
using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Infrastructure.Identity.Data;

namespace AbsoluteCinema.Application.Mappings;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Маппер регистрации
            CreateMap<RegisterDto, ApplicationUser>()
                // Username (от Identity) куда пихаем? 
                .ForMember(dest => dest.UserName,     opt => opt.MapFrom(src => src.Email))
                
                .ForMember(dest => dest.Email,        opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName,    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,     opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDate,    opt => opt.MapFrom(src => src.BirthDate))
                
                // Игнорируем поля от Identity (ставятся автоматически)
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Id,           opt => opt.Ignore());

            // Маппер логина
            CreateMap<LoginDto, ApplicationUser>()
                // Username (от Identity) куда пихаем? 
                .ForMember(dest => dest.UserName,     opt => opt.MapFrom(src => src.Email))
                
                .ForMember(dest => dest.Email,        opt => opt.MapFrom(src => src.Email))
                
                // Игнорируем поля от Identity (ставятся автоматически)
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Id,           opt => opt.Ignore())
                .ForMember(dest => dest.FirstName,    opt => opt.Ignore())
                .ForMember(dest => dest.LastName,     opt => opt.Ignore())
                .ForMember(dest => dest.BirthDate,    opt => opt.Ignore());
            
        }
    }

