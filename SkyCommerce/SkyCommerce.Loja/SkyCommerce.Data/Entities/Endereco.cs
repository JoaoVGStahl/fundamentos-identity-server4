using SkyCommerce.Models;

namespace SkyCommerce.Data.Entities
{
    internal class Endereco
    {
        public int Id { get; set; }
        public string Usuario { get; set; }

        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Referencia { get; set; }
        public string NomeEndereco { get; set; }
        public string QuemRecebe { get; set; }
        public TipoEndereco TipoEndereco { get; set; }

        public Endereco AtualizarUsuario(string usuario)
        {
            this.Usuario = usuario;
            return this;
        }
    }
}
