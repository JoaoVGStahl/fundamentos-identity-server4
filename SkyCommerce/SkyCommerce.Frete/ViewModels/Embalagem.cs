using System.ComponentModel.DataAnnotations;
using Bogus;

namespace SkyCommerce.Fretes.ViewModels
{
    public class EmbalagemViewModel
    {
        [Required]
        public double Altura { get; set; }
        [Required]
        public double Largura { get; set; }
        [Required]
        public double Comprimento { get; set; }
        [Required]
        public double Peso { get; set; }

    }
}
