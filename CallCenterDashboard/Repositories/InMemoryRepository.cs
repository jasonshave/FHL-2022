using CallCenterDashboard.Interfaces;
using System.Collections.Concurrent;

namespace CallCenterDashboard.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        private IDictionary<string, T> _data;

        public InMemoryRepository()
        {
            _data = new ConcurrentDictionary<string, T>();
        }

        public void Add(string key, T value)
        {
            _data.TryAdd(key, value);
        }

        public void Remove(string key)
        {
            _data.Remove(key);
        }

        public T Get(string key)
        {
            T value = default;
            _data.TryGetValue(key, out value);
            return value;
        }

        public void Update(string key, T value)
        {
            _data[key] = value;
        }

        public IEnumerable<KeyValuePair<string, T>>GetAll()
        {
            return _data.Select(record => record);
        }
    }
}
