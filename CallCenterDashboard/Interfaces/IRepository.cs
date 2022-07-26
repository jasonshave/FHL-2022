namespace CallCenterDashboard.Interfaces
{
    public interface IRepository<T>
    {
        void Add(string key, T value);
        void Update(string key, T value);
        void Remove(string key);
        T Get(string key);
        IEnumerable<KeyValuePair<string, T>> GetAll();
    }
}
