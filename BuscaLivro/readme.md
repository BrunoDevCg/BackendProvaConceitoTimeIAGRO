Desafio Busca de Livros

Este projeto foi desenvolvido como parte de um desafio técnico, com foco em manipulação de dados, busca, 
ordenação e cálculo de frete para livros. O projeto foi estruturado utilizando o padrão de camadas para 
facilitar a manutenção e escalabilidade.
Tecnologias Utilizadas

    .NET 8.0: Plataforma principal utilizada para desenvolvimento da API.

    XUnit: Framework de testes utilizado para validar a funcionalidade da aplicação.

    ASP.NET Core: Framework para criar a API.

Estrutura do Projeto

O projeto está estruturado da seguinte forma:

/BuscaLivro
│
├── Controllers
│   └── LivrosController.cs          # Controladores da API
│
├── Data
│   └── livros.json                  # Arquivo de dados de livros
│
├── Domain
│   ├── Entities
│   │   └── Livro.cs                 # Definição da entidade Livro
│   └── Interfaces
│       ├── ILivroRepository.cs      # Interface do repositório
│       └── ILivroService.cs         # Interface do serviço
│
├── Infrastructure
│   └── Repository
│       └── LivroRepository.cs       # Implementação do repositório
│
├── Services
│   └── LivroService.cs              # Implementação dos serviços de busca, ordenação e frete
│
├── Tests
│   └── LivroServiceTests.cs         # Testes automatizados do LivroService
├── appsettings.json                 # Configurações do projeto
├── Program.cs                       # Configuração do projeto e da API

Funcionalidades

    Buscar Livros por Autor, Título ou ID: A aplicação permite buscar livros com base no autor, nome ou ID.

    Ordenar Livros por Preço: Os livros podem ser ordenados por preço, tanto de forma ascendente quanto descendente.

    Buscar Livros frete: A aplicação permite buscar livros com base no autor, nome ou ID, e exibe os valores do livro, frete, e o total(livro+frete).

    API: A aplicação fornece uma API RESTful para realizar buscas e ordenações.

Endpoints da API

    GET /api/livros/buscar?termo={termo}

        Busca livros com base no termo fornecido (pode ser nome do livro, autor ou ID).

    GET /api/livros/ordenar?ordem={asc|desc}

        Ordena os livros por preço, com a opção de ordenação crescente (asc) ou decrescente (desc).

    GET /api/livros/buscar-com-frete?termoBusca={termo}

        Realiza uma busca por livros com frete, calculando o frete de acordo com o preço do livro.



Como Rodar a Aplicação

Execute a aplicação:

    dotnet run


A API estará disponível por padrão em https://localhost:7021/swagger/index.html
Como Rodar os Testes

    Para rodar os testes, navegue até o diretório do projeto e execute:

    dotnet test

    O comando acima irá rodar todos os testes definidos na pasta /Tests e verificar a funcionalidade do serviço.

Testes Implementados

Abaixo estão os testes implementados para garantir a qualidade do código:

    BuscarLivrosComFreteAsync_DeveRetornarLivrosComFrete: Verifica se a busca por livros com frete está funcionando corretamente para um autor existente.

    BuscarLivrosComFreteAsync_DeveRetornarNenhumLivroParaAutorInexistente: Testa se a busca retorna uma lista vazia quando o autor não existe.

    BuscarLivros_DeveRetornarLivroPorNome: Verifica se a busca por nome do livro está funcionando corretamente.

    OrdenarLivrosPorPrecoAsync_DeveRetornarLivrosOrdenadosPorPrecoAsc: Testa se os livros são retornados ordenados por preço de forma crescente.

    OrdenarLivrosPorPrecoAsync_DeveRetornarLivrosOrdenadosPorPrecoDesc: Testa se os livros são retornados ordenados por preço de forma decrescente.
