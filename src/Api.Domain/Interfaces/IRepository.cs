using System;
using System.Threading.Tasks;
using Api.Domain.Entities;
using System.Collections.Generic;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> ExistAsync(Guid id);
        Task<T> SelectAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<T>> SelectAsync();
    }
}
