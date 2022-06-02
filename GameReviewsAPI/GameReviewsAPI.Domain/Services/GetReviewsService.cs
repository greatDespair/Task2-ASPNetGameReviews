using AutoMapper;
using GameReviewsAPI.Data.Dto;
using GameReviewsAPI.Data.Models;
using GameReviewsAPI.Domain.Abstractions;
using GameReviewsAPI.Domain.Repositories;

namespace GameReviewsAPI.Domain.Services
{
    public class GetReviewsService
    {
        
        private IRepository<Game> _gamesDb;
        private IRepository<Review> _reviewsDb;
        private IMapper _mapper;
        public GetReviewsService(IRepository<Game> gameRepository, IRepository<Review> reviewRepository, IMapper mapper)
        {
            _gamesDb = gameRepository;
            _reviewsDb = reviewRepository;
            _mapper = mapper;
        }

        public async Task<List<ReviewInfoDto>?> GetReviewsByGameId(long gameId)
        {
            var game = await _gamesDb.GetById(gameId);
            if (game != null)
            {
                var reviews = await _reviewsDb.GetAll();
                List<ReviewInfoDto> result = new List<ReviewInfoDto>();
                var gameReviews = reviews.Where(gameReview => gameReview.Game == game).ToList();
                if (gameReviews.Count > 0)
                {
                    foreach (var review in gameReviews)
                    {
                        ReviewInfoDto reviewDto = new ReviewInfoDto();
                        reviewDto = _mapper.Map<ReviewInfoDto>(review);
                        result.Add(reviewDto);
                    }
                    return result;
                }
            }
            return null;
        }
    }
}
