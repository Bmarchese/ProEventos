using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IRepository<Evento> _repository;
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IRepository<Evento> repository, IEventoRepository eventoRepository)
        {
            _repository = repository;
            _eventoRepository = eventoRepository;
        }


        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _repository.Add(model);

                if (await _repository.SaveChangesAsync())
                {
                    return await _eventoRepository.GetEventoByIdAsync(model.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int id, Evento model)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(model.Id);
                if (evento == null) return null;

                model.Id = id;

                _repository.Update(model);

                if (await _repository.SaveChangesAsync())
                {
                    return await _eventoRepository.GetEventoByIdAsync(model.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(id);
                if (evento == null) throw new Exception("Evento not found.");

                _repository.Delete(evento);

                return await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync(includePalestrantes);

                if (eventos is null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string filter, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosByTemaAsync(filter, includePalestrantes);

                if (eventos is null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(id, includePalestrantes);

                if (evento is null) return null;

                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
