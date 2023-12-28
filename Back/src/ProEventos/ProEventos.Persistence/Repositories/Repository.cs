using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ProEventosContext _context;

        public Repository(ProEventosContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public void DeletreRange(List<T> entities)
        {
            _context.RemoveRange(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            int saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

    }
}
