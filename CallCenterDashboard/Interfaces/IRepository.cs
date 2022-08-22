namespace CallingDashboard.Interfaces
{
    public interface IRepository<T>
    {
        bool Add(string key, T value);
        bool Update(string key, T value);
        bool Remove(string key);
        T? Find(string key);
        IEnumerable<T> List();
    }
}
