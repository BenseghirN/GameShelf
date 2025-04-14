using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Domain.Entities;

namespace GameShelf.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping Entity → DTO
            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.GameTags.Select(gt => gt.Tag.Nom)))
                .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src => src.GamePlatforms.Select(gp => gp.Platform.Nom)))
                .ReverseMap()
                .ForMember(dest => dest.GameTags, opt => opt.Ignore())
                .ForMember(dest => dest.GamePlatforms, opt => opt.Ignore())
                .ForMember(dest => dest.UserGames, opt => opt.Ignore());
            CreateMap<GamePlatform, GamePlatformDto>().ReverseMap();
            CreateMap<GameTag, GameTagDto>().ReverseMap();
            CreateMap<Platform, PlatformDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserGame, UserGameDto>()
                .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Game.Titre));
            CreateMap<UserProposal, UserProposalDto>().ReverseMap();
        }
    }
}