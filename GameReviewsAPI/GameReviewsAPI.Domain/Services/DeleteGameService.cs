using GameReviewsAPI.Data.Models;
using GameReviewsAPI.Domain.Abstractions;
using GameReviewsAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewsAPI.Domain.Services
{
    public class DeleteGameService
    {
        private IRepository<Game> _gamesDb;
        private IRepository<Review> _reviewsDb;
        public DeleteGameService(IRepository<Game> gameRepository, IRepository<Review> reviewRepository)
        {
            _gamesDb = gameRepository;
            _reviewsDb = reviewRepository;
        }

        public async Task<bool> DeleteGameById(long gameId)
        {
            var game = await _gamesDb.GetById(gameId);
            var gameResult = await _gamesDb.Delete(gameId);
            if (gameResult)
            {
                var reviews = await _reviewsDb.GetAll();
                var gameReviews = reviews.Where(gameReview => gameReview.Game == game).ToList();
                if (gameReviews.Count > 0)
                    foreach (var item in gameReviews)
                    {
                        await _reviewsDb.Delete(item.Id);
                    }
                return true;
            }
            return false;
        }
    }
}
