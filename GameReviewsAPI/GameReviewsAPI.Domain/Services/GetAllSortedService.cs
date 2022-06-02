using AutoMapper;
using GameReviewsAPI.Data.Dto;
using GameReviewsAPI.Data.Models;
using GameReviewsAPI.Domain.Abstractions;
using GameReviewsAPI.Domain.Repositories;

namespace GameReviewsAPI.Domain.Services
{
    public class GetAllSortedService
    {
        private IRepository<Game> _gamesDb;
        private IRepository<Review> _reviewsDb;
        private IMapper _mapper;

        public GetAllSortedService(IRepository<Game> gameRepository, IRepository<Review> reviewRepository, IMapper mapper)
        {
            _gamesDb = gameRepository;
            _reviewsDb = reviewRepository;
            _mapper = mapper;
        }

        public async Task<List<GameDto>?> GetAllSortedDesc()
        {
            var games = await _gamesDb.GetAll();
            var reviews = await _reviewsDb.GetAll();
            PriorityQueue<Game, float> sortGamesByRating = new PriorityQueue<Game, float>();
            List<GameDto> sortedList = new List<GameDto>();

            foreach (var item in games)
            {
                var gameReviews = reviews.Where(game => game.Game == item).ToList();
                if (gameReviews.Count > 0)
                    sortGamesByRating.Enqueue(item, -(float)gameReviews.Select(p => p.Rating).Average());
                else
                    sortGamesByRating.Enqueue(item, 0);
            }

            if (sortGamesByRating.Count > 0)
            {
                while (sortGamesByRating.Count > 0)
                {
                    sortedList.Add(_mapper.Map<GameDto>(sortGamesByRating.Dequeue()));
                }
                if (sortedList.Count > 0)
                    return sortedList;
            }
            return null;
        }
    }
}
