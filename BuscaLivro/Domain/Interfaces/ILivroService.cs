using BuscaLivro.Domain.Entities;

namespace BuscaLivro.Domain.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<Livro>> BuscarLivrosAsync(string termoBusca);
        Task<IEnumerable<Livro>> OrdenarLivrosPorPrecoAsync(string ordem);
        decimal CalcularFrete(decimal precoLivro);
        
        Task<IEnumerable<LivroComFreteDto>> BuscarLivroComFreteAsync(string termoBusca);
    }
}
