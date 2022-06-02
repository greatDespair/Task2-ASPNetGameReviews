using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewsAPI.Domain.Abstractions
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T?> GetById(long id);
        public Task<long?> Add(T item);
        public Task<T?> Update(T item);
        public Task<bool> Delete(long id);
    }
}
