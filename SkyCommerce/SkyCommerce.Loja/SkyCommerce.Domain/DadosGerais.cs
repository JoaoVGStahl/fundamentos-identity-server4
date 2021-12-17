using Bogus;
using SkyCommerce.Models;

namespace SkyCommerce
{
    public static class DadosGerais
    {
        static DadosGerais()
        {
            var faker = new Faker();
            TelefoneSuporte = faker.Phone.PhoneNumber("(##) ####-####");
            EmailSuporte = "suporte@skycommerce.com";
        }
        public static string TelefoneSuporte { get; }
        public static string EmailSuporte { get; }
        public static GeoCoordinate CentroDistribuicao { get; set; } = new GeoCoordinate(-23.6815315, -46.8754801);
    }
}
