namespace ProEventos.Persistence.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeletreRange(List<T> entities);
        Task<bool> SaveChangesAsync();
    }
}
