using CallCenterDashboard.Interfaces;
using System.Collections.Concurrent;

namespace CallCenterDashboard.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly ConcurrentDictionary<string, T> _data = new ();

        public InMemoryRepository(IDictionary<string, T>? preData = null)
        {
            if (preData is not null)
            {
                foreach (var (key, value) in preData)
                {
                    Add(key, value);
                }
            }
        }

        public void Add(string key, T value) => _data.TryAdd(key, value);

        public bool Remove(string key) => _data.TryRemove(key, out _);

        public T? Find(string key)
        {
            _data.TryGetValue(key, out var value);
            return value;
        }

        public bool Update(string key, T value)
        {
            var comparisonValue = Find(key);
            if (comparisonValue is not null)
            {
                return _data.TryUpdate(key, value, comparisonValue);
            }

            return false;
        }

        public IEnumerable<T>List() => _data.Values;
    }
}
