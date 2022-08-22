using CallingDashboard.Interfaces;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace CallingDashboard.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly ConcurrentDictionary<string, T> _data = new();

        public InMemoryRepository(IDictionary<string, T>? preData = null)
        {
            if (preData is null) return;
            foreach (var (key, value) in preData)
            {
                Add(key, value);
            }
        }

        public bool Add(string key, T value) => _data.TryAdd(key, value);

        public bool Remove(string key)
        {
            try
            {
                var result = _data.TryRemove(key, out _);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T? Find(string key)
        {
            _data.TryGetValue(key, out var value);
            return value;
        }

        public bool Update(string key, T value)
        {
            var comparisonValue = Find(key);
            return comparisonValue is not null 
                ? _data.TryUpdate(key, value, comparisonValue) 
                : Add(key, value);
        }

        public IEnumerable<T> List() => _data.Values;
    }
}
