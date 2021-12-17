using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using SkyCommerce.Site.Models;
using SkyCommerce.Site.Service;
using System.Threading.Tasks;

namespace SkyCommerce.Site.Controllers
{
    [Authorize, Route("checkout")]
    public class CheckoutController : Controller
    {
        private readonly IEnderecoStore _enderecoStore;
        private readonly ICarrinhoStore _carrinhoStore;
        private readonly IFreteService _freteService;
        private readonly ICarrinhoService _carrinhoService;
        private readonly IGeoposicaoService _geoposicaoService;
        private readonly IPedidoService _pedidoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutController(
            IEnderecoStore enderecoStore,
            ICarrinhoStore carrinhoStore,
            IFreteService freteService,
            ICarrinhoService carrinhoService,
            IGeoposicaoService geoposicaoService,
            IPedidoService pedidoService,
            IHttpContextAccessor httpContextAccessor)
        {
            _enderecoStore = enderecoStore;
            _carrinhoStore = carrinhoStore;
            _freteService = freteService;
            _carrinhoService = carrinhoService;
            _geoposicaoService = geoposicaoService;
            _pedidoService = pedidoService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("dados-pagamento")]
        public async Task<IActionResult> DadosPagamento()
        {
            var enderecosUsuario = await _enderecoStore.ObterDoUsuario(User.Identity.Name);
            var carrinho = await _carrinhoStore.ObterCarrinho(User.Identity.Name);

            if (carrinho == null)
            {
                TempData["ERRO"] = "Não há itens no carrinho";
                return RedirectToAction("Index", "Carrinho");
            }
            if (!carrinho.FreteSelecionado())
            {
                TempData["ERRO"] = "Selecione o frete";
                return RedirectToAction("Index");
            }
            var cartao = CartaoCredito.Obter().Generate();
            return View(new CheckoutViewModel()
            {
                EnderecosUsuario = enderecosUsuario,
                Carrinho = carrinho,
                CartaoCredito = cartao
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("dados-pagamento")]
        public async Task<IActionResult> DadosPagamento(CheckoutViewModel model)
        {
            model.EnderecosUsuario = await _enderecoStore.ObterDoUsuario(User.Identity.Name);
            model.Carrinho = await _carrinhoStore.ObterCarrinho(User.Identity.Name);

            if (model.Carrinho == null)
            {
                TempData["ERRO"] = "Não há itens no carrinho";
                return RedirectToAction("Index", "Carrinho");
            }
            if (!model.IsValid())
            {
                model.Erro = "Concorde com os termos";
                return View(model);
            }


            var pedido = model.GerarPedido(model.Carrinho, model.EnderecosUsuario);
            await _pedidoService.SalvarPedido(pedido, User.Identity.Name);

            await _carrinhoService.LimparCarrinho(User.Identity.Name);

            return View("ConfirmarPedido", pedido);
        }

        public async Task<IActionResult> Index()
        {
            var carrinho = await _carrinhoStore.ObterCarrinho(User.Identity.Name);

            var at = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var fretes = await _freteService.CalcularCarrinho(carrinho, await _geoposicaoService.GeolocalizarUsuario(), at);
            return View(new CheckoutViewModel()
            {
                OpcoesFrete = fretes,
                Carrinho = carrinho,
            });
        }
    }
}
