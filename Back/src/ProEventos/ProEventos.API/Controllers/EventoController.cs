using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService) 
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = _eventoService.GetAllEventosAsync(true);
                if(eventos is null) return NotFound("Eventos not found.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Eventos data. Error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = _eventoService.GetEventoByIdAsync(id, true);
                if (evento is null) return NotFound("Evento not found by Id.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Eventos data. Error: {ex.Message}");
            }
        }


        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (eventos is null) return NotFound("Eventos not found by Tema.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Eventos data. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostEvento(Evento model)
        {
            try
            {
                var newEvento = await _eventoService.AddEvento(model);

                if (newEvento is null) return BadRequest("Error while trying to add Evento.");

                return StatusCode(StatusCodes.Status201Created, newEvento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while trying to add Evento. Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, Evento model)
        {
            try
            {
                var newEvento = await _eventoService.UpdateEvento(id, model);

                if (newEvento is null) return BadRequest("Error while trying to update Evento.");

                return Ok(newEvento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while attempting to update Evento. Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleted = await _eventoService.DeleteEvento(id);
                if (deleted) return BadRequest("Error while trying to delete Evento.");
                else return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error while attempting to delete Evento. Error: {ex.Message}");
            }
        }

    }
}
