using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Persistence.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ProEventosContext _context;

        public EventoRepository(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                eventos
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            eventos = eventos.OrderBy(e => e.Id);

            return await eventos.ToArrayAsync();
        }


        public async Task<Evento[]> GetAllEventosByTemaAsync(string filter, bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                eventos
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }


            eventos = eventos
                .Where(e => e.Tema.ToLower().Contains(filter.ToLower()))
                .OrderBy(e => e.Id);

            return await eventos.ToArrayAsync();
        }


        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false)
        {
            IQueryable<Evento> eventos = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                eventos
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            return await eventos.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
