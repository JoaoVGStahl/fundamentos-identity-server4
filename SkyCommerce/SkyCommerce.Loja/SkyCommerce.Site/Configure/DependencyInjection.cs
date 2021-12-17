using Microsoft.Extensions.DependencyInjection;
using SkyCommerce.CrossCutting.Services;
using SkyCommerce.Data.Store;
using SkyCommerce.Interfaces;
using SkyCommerce.Services;
using SkyCommerce.Site.Service;

namespace SkyCommerce.Site.Configure
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureSkyCommerce(this IServiceCollection services)
        {
            services.AddScoped<IProdutoStore, ProdutoStore>();
            services.AddScoped<ICategoriaStore, CategoriaStore>();
            services.AddScoped<ICarrinhoStore, CarrinhoStore>();
            services.AddScoped<IMarcaStore, MarcaStore>();
            services.AddScoped<IAvaliacaoStore, AvaliacaoStore>();
            services.AddScoped<IEnderecoStore, EnderecoStore>();
            services.AddScoped<IPedidoStore, PedidoStore>();
            services.AddScoped<IPedidoService, PedidoService>();

            services.AddScoped<IUserGeoLocation, GeoLocateUsers>();
            services.AddScoped<IGeoposicaoService, GeoposicaoService>();
            services.AddScoped<IFreteService, FreteService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICarrinhoService, CarrinhoService>();
            return services;
        }
    }
}
