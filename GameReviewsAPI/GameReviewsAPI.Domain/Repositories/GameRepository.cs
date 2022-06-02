using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReviewsAPI.Data;
using GameReviewsAPI.Domain.Abstractions;
using GameReviewsAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameReviewsAPI.Domain.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private readonly GameReviewsContext _gameDbContext;
        
        public GameRepository(GameReviewsContext context)
        {
            this._gameDbContext = context;
        }
        public async Task<long?> Add(Game item)
        {
            var result = await _gameDbContext.Games.AddAsync(item);
            await _gameDbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<bool> Delete(long id)
        {
            var result = await _gameDbContext.Games
                .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _gameDbContext.Games.Remove(result);
                await _gameDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _gameDbContext.Games.ToListAsync();
        }

        public async Task<Game?> GetById(long id)
        {
            return await _gameDbContext.Games
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Game?> Update(Game item)
        {
            var result = await _gameDbContext.Games
                .FirstOrDefaultAsync(e => e.Id == item.Id);

            if (result != null)
            {
                result.Name = item.Name;
                result.Genre = item.Genre;
                await _gameDbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
