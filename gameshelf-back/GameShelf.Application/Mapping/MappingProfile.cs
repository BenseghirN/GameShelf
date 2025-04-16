using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Domain.Entities;

namespace GameShelf.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    src.GameTags.Select(gt => new TagDto
                    {
                        Id = gt.Tag.Id,
                        Nom = gt.Tag.Nom
                    }).ToList()
                ))
                .ForMember(dest => dest.Platforms, opt => opt.MapFrom(src =>
                    src.GamePlatforms.Select(gp => new PlatformDto
                    {
                        Id = gp.Platform.Id,
                        Nom = gp.Platform.Nom,
                        ImagePath = gp.Platform.ImagePath
                    }).ToList()
                ))
                .ReverseMap()
                .ForMember(dest => dest.GameTags, opt => opt.Ignore())
                .ForMember(dest => dest.GamePlatforms, opt => opt.Ignore())
                .ForMember(dest => dest.UserGames, opt => opt.Ignore());

            CreateMap<Platform, PlatformDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserGame, UserGameDto>()
                .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Game.Titre))
                .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game));
            CreateMap<UserProposal, UserProposalDto>().ReverseMap();
        }
    }
}