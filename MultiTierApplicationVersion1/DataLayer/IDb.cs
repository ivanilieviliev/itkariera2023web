using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDb<T, K> //where K : IConvertible // for wanna be developers
    {
        Task CreateAsync(T item);

        Task<T> ReadAsync(K key, bool useNavigationProperties = false);

        Task<List<T>> ReadAllAsync(bool useNavigationProperties = false);

        Task UpdateAsync(T item, bool useNavigationProperties = false);

        Task DeleteAsync(K key);

    }
}
