using Bogus;
using System;

namespace SkyCommerce.Models
{
    public class Avaliacao
    {
        public string Usuario { get; set; }
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;
        public string Comentario { get; set; }
        public int Nota { get; set; }
        public string ProdutoUrl { get; set; }
        public string Titulo { get; set; }
        public string Imagem { get; set; }

        public static Faker<Avaliacao> Obter(string produtoUrl)
        {
            return new Faker<Avaliacao>()
                .RuleFor(a => a.Usuario, f => f.Person.FullName)
                .RuleFor(a => a.DataAvaliacao, f => f.Date.Past())
                .RuleFor(a => a.Comentario, f => f.Lorem.Paragraphs())
                .RuleFor(a => a.Titulo, f => f.Lorem.Sentence())
                .RuleFor(a => a.Imagem, f => f.Image.LoremFlickrUrl(keywords: "people", width: 70, height: 70))
                .RuleFor(a => a.Nota, f => f.Random.Int(0, 5))
                .RuleFor(a => a.ProdutoUrl, produtoUrl);
        }
    }
}