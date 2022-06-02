using GameReviewsAPI.Domain.Abstractions;
using GameReviewsAPI.Data;
using GameReviewsAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameReviewsAPI.Domain.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly GameReviewsContext _reviewDbContext;
        public ReviewRepository(GameReviewsContext context)
        {
            _reviewDbContext = context;
        }
        public async Task<long?> Add(Review item)
        {
            var result = await _reviewDbContext.Games
                .FirstOrDefaultAsync(e => e.Id == item.Game.Id);
            if (result != null)
            {
                await _reviewDbContext.Reviews.AddAsync(item);
                await _reviewDbContext.SaveChangesAsync(); 
            }
            return result?.Id;
        }

        public async Task<bool> Delete(long id)
        {
            var result = await _reviewDbContext.Reviews
                .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _reviewDbContext.Reviews.Remove(result);
                await _reviewDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _reviewDbContext.Reviews.ToListAsync();
        }

        public async Task<Review?> GetById(long id)
        {
            return await _reviewDbContext.Reviews
              .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Review?> Update(Review item)
        {
            var result = await _reviewDbContext.Reviews
                .FirstOrDefaultAsync(e => e.Id == item.Id);

            if (result != null)
            {
                result.ReviewText = item.ReviewText;
                result.Game = item.Game;
                result.Rating = item.Rating;
                await _reviewDbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
