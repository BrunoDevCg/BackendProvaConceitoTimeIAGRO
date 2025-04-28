using BuscaLivro.Domain.Entities;
using BuscaLivro.Domain.Interfaces;
using BuscaLivro.Services;
using Xunit;

namespace BuscaLivro.Tests
{
    public class LivroServiceTests
    {
        // Implementação fake do repositório
        private class LivroRepositoryFake : ILivroRepository
        {
            public Task<IEnumerable<Livro>> ObterTodosAsync()
            {
                // Retorna uma lista fake de livros
                return Task.FromResult<IEnumerable<Livro>>(new List<Livro>
                {
                    new Livro(1, "Livro A", 50.00m, new Specifications("2020", "Autor A", 200, new List<string> { "Ilustrador A" }, new List<string> { "Ficção" })),
                    new Livro(2, "Livro B", 30.00m, new Specifications("2021", "Autor B", 150, new List<string> { "Ilustrador B" }, new List<string> { "Aventura" })),
                    new Livro(3, "Livro C", 70.00m, new Specifications("2022", "Autor A", 220, new List<string> { "Ilustrador C" }, new List<string> { "Ficção" }))
                });
            }
        }

        // Teste 1: Verificar se o serviço retorna livros com frete quando um autor válido é passado
        [Fact]
        public async Task BuscarLivrosComFreteAsync_DeveRetornarLivrosComFrete()
        {
            // Arrange
            var livroRepositoryFake = new LivroRepositoryFake();
            var livroService = new LivroService(livroRepositoryFake);

            // Act
            var resultado = await livroService.BuscarLivroComFreteAsync("Autor A");

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count()); // Espera-se 2 livros com "Autor A"
            Assert.Equal(60.00m, resultado.First().Total);  // Verificando o preço total do primeiro livro
        }

        // Teste 2: Verificar se o serviço retorna uma lista vazia para autor inexistente
        [Fact]
        public async Task BuscarLivrosComFreteAsync_DeveRetornarNenhumLivroParaAutorInexistente()
        {
            // Arrange
            var livroRepositoryFake = new LivroRepositoryFake();
            var livroService = new LivroService(livroRepositoryFake);

            // Act
            var resultado = await livroService.BuscarLivroComFreteAsync("Autor Inexistente");

            // Assert
            Assert.Empty(resultado);  // Nenhum livro deve ser retornado
        }

        // Teste 3: Verificar se a busca por nome de livro funciona corretamente
        [Fact]
        public async Task BuscarLivros_DeveRetornarLivroPorNome()
        {
            // Arrange
            var livroRepositoryFake = new LivroRepositoryFake();
            var livroService = new LivroService(livroRepositoryFake);

            // Act
            var resultado = await livroService.BuscarLivrosAsync("Livro A");

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);  // Espera-se um único livro com o nome "Livro A"
            Assert.Equal("Livro A", resultado.First().Name);  // Verifica o nome do livro
        }

        // Teste 4: Verificar se a ordenação dos livros por preço está funcionando
        [Fact]
        public async Task OrdenarLivrosPorPrecoAsync_DeveRetornarLivrosOrdenadosPorPrecoAsc()
        {
            // Arrange
            var livroRepositoryFake = new LivroRepositoryFake();
            var livroService = new LivroService(livroRepositoryFake);

            // Act
            var resultado = await livroService.OrdenarLivrosPorPrecoAsync("asc");

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count()); // Espera-se 3 livros
            Assert.Equal(30.00m, resultado.First().Price);  // O livro com menor preço deve estar primeiro
        }

        // Teste 5: Verificar a ordenação dos livros por preço descendente
        [Fact]
        public async Task OrdenarLivrosPorPrecoAsync_DeveRetornarLivrosOrdenadosPorPrecoDesc()
        {
            // Arrange
            var livroRepositoryFake = new LivroRepositoryFake();
            var livroService = new LivroService(livroRepositoryFake);

            // Act
            var resultado = await livroService.OrdenarLivrosPorPrecoAsync("desc");

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count()); // Espera-se 3 livros
            Assert.Equal(70.00m, resultado.First().Price);  // O livro com maior preço deve estar primeiro
        }
    }
}
