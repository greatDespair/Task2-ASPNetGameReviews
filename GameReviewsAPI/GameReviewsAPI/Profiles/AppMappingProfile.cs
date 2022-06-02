using AutoMapper;
using GameReviewsAPI.Data.Dto;
using GameReviewsAPI.Data.Models;

namespace GameReviewsAPI
{
    public class AppMappingProfile:Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();
            CreateMap<GameInfoDto, Game>();
            CreateMap<Game, GameInfoDto>();
            CreateMap<Review, ReviewInfoDto>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Game.Id));
        }
    }
}
