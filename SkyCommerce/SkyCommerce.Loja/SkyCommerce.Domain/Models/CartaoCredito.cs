using Bogus;
using SkyCommerce.Extensions;

namespace SkyCommerce.Models
{
    public class CartaoCredito
    {
        public CartaoCredito() { }
        private string _numero;
        private string _cvc;

        public string Numero
        {
            get => _numero.TruncateCreditCard();
            set => _numero = value;
        }

        public string Nome { get; set; }
        public string Mes { get; set; }

        public string Ano { get; set; }
        public string CodigoVerificador
        {
            get => _cvc.TruncateSensitiveInformation();
            set => _cvc = value;
        }

        public static Faker<CartaoCredito> Obter()
        {
            return new Faker<CartaoCredito>()
                .RuleFor(c => c.Numero, f => f.Finance.CreditCardNumber())
                .RuleFor(c => c.Nome, f => f.Person.FullName)
                .RuleFor(c => c.Ano, f => f.Date.Future(5).ToString("yyyy"))
                .RuleFor(c => c.Mes, f => f.Date.Future(5).ToString("MM"))
                .RuleFor(c => c.CodigoVerificador, f => f.Finance.CreditCardCvv());
        }

#if DEBUG
        public string SistemaEmTeste_ObterNumeroCompleto()
        {
            return _numero;
        }
        public string SistemaEmTeste_ObterCodigoVerificador()
        {
            return _cvc;
        }
#endif
    }
}