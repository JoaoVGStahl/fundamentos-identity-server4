using SkyCommerce.Data.Util;
using SkyCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SkyCommerce.ViewObjects;

namespace SkyCommerce.Site.Models
{
    public class CheckoutViewModel
    {
        public IEnumerable<Endereco> EnderecosUsuario { get; set; }

        public bool UtilizarEnderecoCadastrado { get; set; }
        public string NomeEnderecoCadastrado { get; set; }
        public Endereco EnderecoEntrega { get; set; }
        public bool EnderecoPagamentoMesmoEntrega { get; set; }
        public Endereco EnderecoCobranca { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public bool ConcordouComTermosECondicoes { get; set; }
        public CartaoCredito CartaoCredito { get; set; }
        public Carrinho Carrinho { get; set; }
        public string Comentario { get; set; }
        public string Erro { get; set; }
        public IEnumerable<Frete> OpcoesFrete { get; set; }

        public Pedido GerarPedido(Carrinho carrinho, IEnumerable<Endereco> enderecos)
        {
            if (!ConcordouComTermosECondicoes)
                return null;

            if (UtilizarEnderecoCadastrado)
                EnderecoEntrega = enderecos.First(f => string.Equals(f.NomeEndereco.ToLower(), NomeEnderecoCadastrado.ToLower(), StringComparison.Ordinal));

            if (EnderecoPagamentoMesmoEntrega)
            {
                var novoEndereco = new Endereco();
                novoEndereco.ShallowCopyTo(EnderecoEntrega);
                EnderecoCobranca = novoEndereco;

            }
            if (Carrinho.TotalProdutos > 200)
                carrinho.Frete = new Frete() { Modalidade = "Gratis", Descricao = "Gratuito acima de R$200", Valor = 0 };

            return new Pedido(Carrinho.Frete, EnderecoCobranca, EnderecoEntrega, CartaoCredito, TipoPagamento, carrinho, Comentario);
        }

        public bool IsValid()
        {
            return ConcordouComTermosECondicoes;
        }
    }
}
