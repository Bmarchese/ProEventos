using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IPalestranteRepository
    {
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string filter, bool includeEventos = false);
        Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEventos = false);
    }
}
