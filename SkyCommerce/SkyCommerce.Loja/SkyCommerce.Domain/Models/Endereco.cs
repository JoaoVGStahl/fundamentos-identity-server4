using Bogus;

namespace SkyCommerce.Models
{
    public class Endereco
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Referencia { get; set; }
        public string NomeEndereco { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
        public Telefone Telefone { get; set; }
        public static Faker<Endereco> Obter(string nome)
        {
            return new Faker<Endereco>("pt_BR")
                .RuleFor(e => e.Logradouro, f => f.Address.StreetAddress())
                .RuleFor(e => e.Bairro, f => f.Address.SecondaryAddress())
                .RuleFor(e => e.Cidade, f => f.Address.City())
                .RuleFor(e => e.Estado, f => f.Address.State())
                .RuleFor(e => e.Cep, f => f.Address.ZipCode("?????-???"))
                .RuleFor(e => e.Referencia, f => f.Lorem.Word())
                .RuleFor(e => e.NomeEndereco, f => f.Lorem.Word())
                .RuleFor(e => e.Telefone, f => f.Phone.PhoneNumber("(##) 9####-####"))
                .RuleFor(e => e.Nome, nome)
                .RuleFor(e => e.TipoEndereco, f => f.PickRandom<TipoEndereco>());
        }
    }
}