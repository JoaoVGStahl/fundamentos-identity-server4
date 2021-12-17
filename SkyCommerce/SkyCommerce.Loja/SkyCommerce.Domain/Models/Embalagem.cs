using Bogus;

namespace SkyCommerce.Models
{
    public class Embalagem
    {
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Peso { get; set; }

        public static Faker<Embalagem> Obter()
        {
            return new Faker<Embalagem>()
                .RuleFor(e => e.Altura, f => f.Random.Double() * f.Random.Int(1, 54))
                .RuleFor(e => e.Largura, f => f.Random.Double() * f.Random.Int(1, 54))
                .RuleFor(e => e.Comprimento, f => f.Random.Double() * f.Random.Int(1, 54))
                .RuleFor(e => e.Peso, f => f.Random.Double() * f.Random.Int(1, 20));
        }
    }
}