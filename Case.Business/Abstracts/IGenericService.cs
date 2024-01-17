namespace LibraryImplement.Business.Abstracts
{
    public interface IGenericService<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        List<T> GetList();
        T? GetByID(int id);
    }
}
