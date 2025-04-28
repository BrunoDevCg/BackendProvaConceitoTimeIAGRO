using BuscaLivro.Domain.Entities;

namespace BuscaLivro.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ObterTodosAsync();
    }
}