using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SkyCommerce.Data.Context;
using SkyCommerce.Data.Util;
using SkyCommerce.Interfaces;
using System;
using System.Threading.Tasks;

namespace SkyCommerce.Site.Configure
{
    public class DatabaseChecker
    {
        public static async Task EnsureDatabaseIsReady(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<SkyContext>();

            Log.Information("Testing conection with database");
            await DbHealthChecker.TestConnection(context);
            Log.Information("Connection successfull");


            Log.Information("Aguarde, carregando base de dados");
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var carrinhoService = scope.ServiceProvider.GetRequiredService<ICarrinhoService>();

            Log.Information("Carregando produtos");
            var produtos = await FakeData.CarregarProdutos(context, env.WebRootPath);

            Log.Information("Carregando categorias");
            await FakeData.CarregarCategorias(context, produtos);

            Log.Information("Populando carrinho");
            await FakeData.PopularCarrinho(produtos, carrinhoService, configuration.GetValue<string>("ApplicationSettings:DefaultUser") ?? "bob");

            Log.Information("Gerando Enderecos");
            await FakeData.GerarEnderecos(context, configuration);

            Log.Information("Gerando Pedidos");
            await FakeData.GerarPedidos(context, configuration, env.WebRootPath);

            Log.Information("Dados carregados");
        }
    }
}
