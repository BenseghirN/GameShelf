using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Domain.Entities;

namespace GameShelf.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mappings pour l'autentification
            CreateMap<User, AuthResultDto>();
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            //Création de l'utilisateur par un admin
            CreateMap<AdminCreateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Mapping Entity → DTO
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<GamePlatform, GamePlatformDto>().ReverseMap();
            CreateMap<GameTag, GameTagDto>().ReverseMap();
            CreateMap<Platform, PlatformDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserGame, UserGameDto>().ReverseMap();
            CreateMap<UserProposal, UserProposalDto>().ReverseMap();
        }
    }
}