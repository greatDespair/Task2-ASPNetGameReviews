using GameReviewsAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameReviewsAPI.Data
{
    public class GameReviewsContext: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public GameReviewsContext(DbContextOptions<GameReviewsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
