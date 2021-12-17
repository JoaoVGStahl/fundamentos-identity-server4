using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using SkyCommerce.Site.Models;
using SkyCommerce.ViewObjects;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SkyCommerce.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoStore _produtoStore;
        private readonly ICategoriaStore _categoriaStore;
        private readonly IMarcaStore _marcaStore;
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICarrinhoStore _carrinhoStore;

        public HomeController(ILogger<HomeController> logger,
            IProdutoStore produtoStore,
            ICategoriaStore categoriaStore,
            IMarcaStore marcaStore,
            ICarrinhoService carrinhoService,
            ICarrinhoStore carrinhoStore)
        {
            _logger = logger;
            _produtoStore = produtoStore;
            _categoriaStore = categoriaStore;
            _marcaStore = marcaStore;
            _carrinhoService = carrinhoService;
            _carrinhoStore = carrinhoStore;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _produtoStore.ObterTodos();
            var categorias = await _categoriaStore.ObterTodos();
            var marcas = await _marcaStore.ObterTodos();

            return View(new ListarProdutosViewModel()
            {
                NovoProdutos = model.Where(w => w.Novo && !w.Promocao).Take(5),
                Vitrine = model.OrderByDescending(o => o.Valor).Take(5),
                Categorias = categorias.Take(6),
                ProdutosEmDestaque = model.OrderByDescending(o => o.ValorAntigo).Take(8),
                Marcas = marcas,
            });
        }

        [Route("lista")]
        public async Task<IActionResult> Lista([FromQuery] PesquisarProdutoVo model)
        {

            return View("Produtos/_ListaProdutos", new ProdutosPrincipalViewModel()
            {
                Produtos = await _produtoStore.Pesquisar(model),
                Categorias = await _categoriaStore.ObterTodos(),
                Marcas = await _marcaStore.ObterTodos(),
                PesquisaAtual = model,
                Action = "Lista",
                Controller = "Home"
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
