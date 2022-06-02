using GameReviewsAPI.Data.Dto;
using GameReviewsAPI.Data.Models;
using GameReviewsAPI.Domain.Abstractions;
using GameReviewsAPI.Domain.Repositories;

namespace GameReviewsAPI.Domain.Services
{
    public class AddReviewService
    {
        private IRepository<Game> _gamesDb;
        private IRepository<Review> _reviewsDb;
        public AddReviewService(IRepository<Game> gameRepository, IRepository<Review> reviewRepository)
        {
            _gamesDb = gameRepository;
            _reviewsDb = reviewRepository;
        }

        public async Task<long?> GetReviewsByGameId(ReviewInfoDto reviewDto)
        {
            if (!((reviewDto.Rating <= 5f) && (reviewDto.Rating >= 0)))
                return null;
            Review review = new Review();
            review.ReviewText = reviewDto.ReviewText;
            review.Rating = reviewDto.Rating;
            var game = await _gamesDb.GetById(reviewDto.GameId);
            if (game != null)
            {
                review.Game = game;
                await _reviewsDb.Add(review);
                return review.Id;
            }
            return null;
        }
    }
}
