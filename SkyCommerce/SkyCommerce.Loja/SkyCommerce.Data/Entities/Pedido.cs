using SkyCommerce.Models;
using System;
using System.Collections.Generic;
using SkyCommerce.ViewObjects;

namespace SkyCommerce.Data.Entities
{
    internal class Pedido
    {
        public int Id { get; set; }
        public string Usuario { get; set; }

        public Models.Endereco EnderecoEntrega { get; set; }
        public Models.Endereco EnderecoCobranca { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public CartaoCredito Cartao { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public DateTime DataPagamento { get; set; }
        public string IdentificadorUnico { get; set; }
        public ICollection<ProdutoVendido> Produtos { get; set; }
        public Frete Frete { get; set; }
        public string Cupom { get; set; }
        public decimal Desconto { get; set; }
        public string Comentarios { get; set; }
        public string RastreamentoFrete { get; set; }

        public Pedido AtualizarUsuario(string usuario)
        {
            Usuario = usuario;
            return this;
        }
    }
}
