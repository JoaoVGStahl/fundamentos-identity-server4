using System;
using System.Text.RegularExpressions;

namespace SkyCommerce.Models
{
    public struct Telefone
    {
        private static Regex _ddd = new Regex("\\([0-9]{2}\\)");
        private static Regex _numero = new Regex("(?<=\\([0-9]{2}\\))(.|[0-9]|-){0,11}$");
        private Telefone(string numero)
        {
            if (numero.IndexOfAny(new[] { '(', ')' }) < 0)
                throw new ArgumentException(nameof(numero));

            Numero = _numero.Match(numero).Value?.Trim();
            DDD = _ddd.Match(numero).Value?.Replace("(", string.Empty).Replace(")", string.Empty);
        }
        public string Numero { get; set; }
        public string DDD { get; set; }

        public static implicit operator Telefone(string value)
            => new Telefone(value);

        public override string ToString() => $"({DDD}) {Numero}";
    }
}