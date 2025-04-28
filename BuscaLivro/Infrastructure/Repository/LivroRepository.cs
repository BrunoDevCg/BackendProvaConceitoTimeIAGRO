using BuscaLivro.Domain.Entities;
using BuscaLivro.Domain.Interfaces;
using System.Text.Json;

namespace BuscaLivro.Infrastructure.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly string _caminhoArquivoJson;

        public LivroRepository()
        {
            _caminhoArquivoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "livros.json");
        }

        public async Task<IEnumerable<Livro>> ObterTodosAsync()
        {
            if (!File.Exists(_caminhoArquivoJson))
                throw new FileNotFoundException("Arquivo JSON não encontrado.");

            var json = await File.ReadAllTextAsync(_caminhoArquivoJson);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var livros = JsonSerializer.Deserialize<IEnumerable<Livro>>(json, options);

            return livros ?? new List<Livro>();
        }
    }
}
