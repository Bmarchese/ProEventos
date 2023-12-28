using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Repositories
{
    public class PalestranteRepository : IPalestranteRepository
    {
        private readonly ProEventosContext _context;

        public PalestranteRepository(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> palestrantes = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                palestrantes
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            palestrantes = palestrantes.OrderBy(p => p.Id);

            return await palestrantes.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string filter, bool includeEventos = false)
        {
            IQueryable<Palestrante> palestrantes = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                palestrantes
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            palestrantes = palestrantes
                .Where(p => p.Nome.ToLower().Contains(filter.ToLower()))
                .OrderBy(p => p.Id);

            return await palestrantes.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEventos = false)
        {
            IQueryable<Palestrante> palestrantes = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                palestrantes
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            return await palestrantes.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
