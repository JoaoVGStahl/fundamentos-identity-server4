using Microsoft.Extensions.Logging;
using Refit;
using SkyCommerce.Extensions;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using SkyCommerce.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkyCommerce.Services
{
    public class FreteService : IFreteService
    {
        private readonly IProdutoStore _produtoStore;
        private readonly ILogger<FreteService> _logger;

        public FreteService(IProdutoStore produtoStore, ILogger<FreteService> logger)
        {
            _produtoStore = produtoStore;
            _logger = logger;
        }


        public Task<IEnumerable<DetalhesFrete>> ObterModalidades(string token)
        {
            var freteApi = RestService.For<IFreteApi>("https://localhost:5007");
            return freteApi.Modalidades($"Bearer {token}");
        }

        public Task<IEnumerable<Frete>> CalcularFrete(Embalagem embalagem, GeoCoordinate posicao, string token)
        {
            var httpClient = new HttpClient(new HttpLoggingHandler(_logger)) { BaseAddress = new Uri("https://localhost:5007") };
            var freteApi = RestService.For<IFreteApi>(httpClient);
            return freteApi.Calcular(posicao.Latitude, posicao.Longitude, embalagem, $"Bearer {token}");
        }

        public async Task<IEnumerable<Frete>> CalcularCarrinho(Carrinho carrinho, GeoCoordinate posicao, string token)
        {
            var httpClient = new HttpClient(new HttpLoggingHandler(_logger)) { BaseAddress = new Uri("https://localhost:5007") };
            var freteApi = RestService.For<IFreteApi>(httpClient);
            var fretes = (await freteApi.Modalidades($"Bearer {token}")).Select(Frete.FromViewModel).ToList();
            if (carrinho != null && posicao != null)
            {
                foreach (var carrinhoItem in carrinho.Items)
                {
                    var produto = await _produtoStore.ObterPorNome(carrinhoItem.NomeUnico);
                    var opcoesDeFrete = await freteApi.Calcular(posicao.Latitude, posicao.Longitude, produto.Embalagem, $"Bearer {token}");
                    foreach (var frete in fretes)
                    {
                        frete.AtualizarValor(opcoesDeFrete.Modalidade(frete.Modalidade));
                    }
                }
            }

            return fretes;
        }
    }
}
