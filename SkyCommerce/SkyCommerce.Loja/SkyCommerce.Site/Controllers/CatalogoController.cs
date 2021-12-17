using Microsoft.AspNetCore.Mvc;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using SkyCommerce.Site.Models;
using SkyCommerce.Site.Service;
using SkyCommerce.ViewObjects;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace SkyCommerce.Site.Controllers
{
    [Route("catalogo")]
    public class CatalogoController : Controller
    {
        private readonly IProdutoStore _produtoStore;
        private readonly IGeoposicaoService _geoposicaoService;
        private readonly IFreteService _freteService;
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaStore _categoriaStore;
        private readonly IMarcaStore _marcaStore;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CatalogoController(
            IProdutoStore produtoStore,
            IGeoposicaoService geoposicaoService,
            IFreteService freteService,
            IProdutoService produtoService,
            ICategoriaStore categoriaStore,
            IMarcaStore marcaStore,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _produtoStore = produtoStore;
            _geoposicaoService = geoposicaoService;
            _freteService = freteService;
            _produtoService = produtoService;
            _categoriaStore = categoriaStore;
            _marcaStore = marcaStore;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [Route("{produto}")]
        public async Task<IActionResult> Detalhes(string produto)
        {
            var produtoDetails = await _produtoStore.ObterPorNome(produto);
            var produtosRelacionados = await _produtoStore.ObterPorCategoria(produtoDetails.Categorias.FirstOrDefault());

            var at = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            var frete = await _freteService.ObterModalidades(at);

            return View(new ProdutoDetalhesViewModel()
            {
                Produto = produtoDetails,
                ProdutosRelacionados = produtosRelacionados.Where(w => w.NomeUnico != produtoDetails.NomeUnico).Take(6),
                OpcoesFrete = frete,
            });
        }

        private async Task<string> GeolocalizarUsuario()
        {
            var posicaoDoGuerreiro = Request.Cookies["geoposicao"];
            if (posicaoDoGuerreiro == null)
            {
                var localizacaoDoUsuario = await _geoposicaoService.ObterLocalizacaoAtual();
                posicaoDoGuerreiro = localizacaoDoUsuario.Latitude + "|" + localizacaoDoUsuario.Longitude;
                Response.Cookies.Append("geoposicao", localizacaoDoUsuario.Latitude + "|" + localizacaoDoUsuario.Longitude);
            }

            return posicaoDoGuerreiro;
        }

        [Route("visualizar/{produto}")]
        public async Task<IActionResult> PreVisualizar(string produto)
        {
            var produtoDetails = await _produtoStore.ObterPorNome(produto);
            return PartialView("Produtos/_Visualizar", produtoDetails);
        }

        [Route("comentar/{produto}")]
        public async Task<IActionResult> Comentar(string produto, [FromForm] Avaliacao avaliacao)
        {
            avaliacao.ProdutoUrl = produto;
            avaliacao.Imagem = "/images/site/default-user.png";
            await _produtoService.Comentar(avaliacao);
            return RedirectToAction("Detalhes", new { produto });
        }


        [Route("marcas/{marca}")]
        public async Task<IActionResult> Marca(string marca, [FromQuery] PesquisarProdutoVo model)
        {
            model.Marca = marca;
            var marcaDetalhes = await _marcaStore.ObterPorNome(marca);

            return View("Produtos/_ListaProdutos", new ProdutosPrincipalViewModel()
            {
                Produtos = await _produtoStore.PesquisarPorMarca(model),
                ImagemCapa = marcaDetalhes?.Imagem,
                Titulo = marcaDetalhes?.Nome,
                SubTitulo = string.Empty,
                Categorias = await _categoriaStore.ObterTodos(),
                Marcas = await _marcaStore.ObterTodos(),
                PesquisaAtual = model,
                Action = "Lista",
                Controller = "Home"
            });
        }
    }
}
