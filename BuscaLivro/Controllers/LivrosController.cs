using BuscaLivro.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuscaLivro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarLivros([FromQuery] string termo)
        {
            var livros = await _livroService.BuscarLivrosAsync(termo);
            return Ok(livros);
        }

        [HttpGet("ordenar")]
        public async Task<IActionResult> OrdenarLivros([FromQuery] string ordem)
        {
            var livros = await _livroService.OrdenarLivrosPorPrecoAsync(ordem);
            return Ok(livros);
        }

        [HttpGet("buscar-com-frete")]
        public async Task<IActionResult> BuscarLivrosComFreteAsync([FromQuery] string termoBusca)
        {
            try
            {
                var livrosComFrete = await _livroService.BuscarLivroComFreteAsync(termoBusca);
                if (livrosComFrete == null || !livrosComFrete.Any())
                {
                    return NotFound("Nenhum livro encontrado.");
                }
                return Ok(livrosComFrete);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar livros: {ex.Message}");
            }
        }
    }

}
