using BuscaLivro.Domain.Entities;
using BuscaLivro.Domain.Interfaces;

namespace BuscaLivro.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<IEnumerable<Livro>> BuscarLivrosAsync(string termoBusca)
        {
            var livros = await _livroRepository.ObterTodosAsync();

            if (string.IsNullOrWhiteSpace(termoBusca))
                return livros;

            termoBusca = termoBusca.ToLower();

            return livros.Where(l =>
                (l.Name != null && l.Name.ToLower().Contains(termoBusca)) ||
                (l.Specifications?.Author != null && l.Specifications.Author.ToLower().Contains(termoBusca)) ||
                (l.Specifications?.Genres != null && l.Specifications.Genres.Any(g => g.ToLower().Contains(termoBusca)))
            );
        }

        public async Task<IEnumerable<Livro>> OrdenarLivrosPorPrecoAsync(string ordem)
        {
            var livros = await _livroRepository.ObterTodosAsync();

            return ordem.ToLower() switch
            {
                "asc" => livros.OrderBy(l => l.Price),
                "desc" => livros.OrderByDescending(l => l.Price),
                _ => livros
            };
        }

        public decimal CalcularFrete(decimal precoLivro)
        {
            return precoLivro * 0.20m;
        }

        // Método para buscar livro por nome, id ou autor e calcular o valor do frete e total
        public async Task<IEnumerable<LivroComFreteDto>> BuscarLivroComFreteAsync(string termoBusca)
        {
            var livros = await _livroRepository.ObterTodosAsync();

            // Filtra os livros com base no termo de busca (nome, autor ou id)
            var livrosEncontrados = livros.Where(l =>
                (l.Name != null && l.Name.ToLower().Contains(termoBusca.ToLower())) ||
                (l.Specifications?.Author != null && l.Specifications.Author.ToLower().Contains(termoBusca.ToLower())) ||
                (l.Id.ToString() == termoBusca) 
            );

            var livrosComFrete = livrosEncontrados.Select(l => new LivroComFreteDto(
                l.Id,
                l.Name,
                l.Specifications.Author,
                l.Price,
                CalcularFrete(l.Price),
                l.Price + CalcularFrete(l.Price)
            ));

            // Retorna uma lista vazia caso nenhum livro seja encontrado
            return livrosComFrete.ToList();
        }


    }
}
