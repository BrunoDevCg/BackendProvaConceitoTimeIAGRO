using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuscaLivro.Domain.Entities
{
    public class Livro
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Specifications Specifications { get; private set; }

        public Livro(int id, string name, decimal price, Specifications specifications)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do livro não pode ser nulo ou vazio.", nameof(name));

            Id = id;
            Name = name;
            Price = price;
            Specifications = specifications ?? throw new ArgumentNullException(nameof(specifications), "As especificações não podem ser nulas.");
        }
    }

    public class LivroComFreteDto
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public decimal Price { get; private set; }
        public decimal Frete { get; private set; }
        public decimal Total { get; private set; }
        
        public LivroComFreteDto(int id, string name, string author, decimal price, decimal frete, decimal total)
        {
            Id = id;
            Name = name;
            Author = author;
            Price = price;
            Frete = frete;
            Total = total;
        }
    }

    public class Specifications
    {
        [JsonPropertyName("Originally published")]
        public string OriginallyPublished { get; private set; }

        [JsonPropertyName("Author")]
        public string Author { get; private set; }

        [JsonPropertyName("Page count")]
        public int PageCount { get; private set; }

        [JsonPropertyName("Illustrator")]
        [JsonConverter(typeof(IllustratorConverter))]
        public List<string> Illustrator { get; private set; }

        [JsonPropertyName("Genres")]
        [JsonConverter(typeof(GenresConverter))]
        public List<string> Genres { get; private set; }

        public Specifications(string originallyPublished, string author, int pageCount, List<string> illustrator, List<string> genres)
        {
            OriginallyPublished = originallyPublished;
            Author = author;
            PageCount = pageCount;
            Illustrator = illustrator ?? new List<string>();
            Genres = genres ?? new List<string>();
        }
    }

    public class IllustratorConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return new List<string> { reader.GetString() };
            }
            else if (reader.TokenType == JsonTokenType.StartArray)
            {
                return JsonSerializer.Deserialize<List<string>>(ref reader, options);
            }

            throw new JsonException("Invalid type for Illustrator");
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }

    public class GenresConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return new List<string> { reader.GetString() };
            }
            else if (reader.TokenType == JsonTokenType.StartArray)
            {
                return JsonSerializer.Deserialize<List<string>>(ref reader, options);
            }

            throw new JsonException("Invalid type for Genres");
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
