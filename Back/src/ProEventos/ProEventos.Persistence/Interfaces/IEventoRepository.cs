using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IEventoRepository
    {
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByTemaAsync(string filter, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false);
    }
}
