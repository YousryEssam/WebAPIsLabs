namespace WebAPIsLabs.Repos
{
    public interface IGenericRepository<T> 
    {
        void Delete(int id);
        List<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Update(T obj);
        void Save();
    }
}
