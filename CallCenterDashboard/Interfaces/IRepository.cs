namespace CallCenterDashboard.Interfaces
{
    public interface IRepository<T>
    {
        void Add(string key, T value);
        bool Update(string key, T value);
        bool Remove(string key);
        T? Find(string key);
        IEnumerable<T> List();
    }
}
