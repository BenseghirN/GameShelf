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