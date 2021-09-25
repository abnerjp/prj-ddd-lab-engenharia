using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class PedidosController : Controller
    {
        private IBaseService<Pedido> _baseService;
        private IBaseService<Restaurante> _baseServiceRestaurante;
        private IBaseService<Produto> _baseServiceProduto;

        private readonly IEnumerable<RestauranteViewModel> Restaurantes;
        private readonly IEnumerable<ProdutoViewModel> Produtos;


        public PedidosController(IBaseService<Pedido> baseService, IBaseService<Restaurante> baseServiceRestaurante, IBaseService<Produto> baseServiceProduto)
        {
            _baseService = baseService;
            _baseServiceRestaurante = baseServiceRestaurante;
            _baseServiceProduto = baseServiceProduto;

            this.Restaurantes = this.getRestaurantes();
            this.Produtos = this.getProdutos();
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var resultado = func();
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        private PedidoViewModel Parse(Pedido pedido)
        {
            return new PedidoViewModel()
            {
                Id = pedido.Id,
                DataPedido = pedido.DataPedido,
                Quantidade = pedido.Quantidade,
                Desconto = pedido.Desconto,
                TaxaFrete = pedido.TaxaFrete,
                ValorTotal = pedido.ValorTotal,
                RestauranteId = pedido.RestauranteId,
                ProdutoId = pedido.ProdutoId
            };
        }

        private Pedido Parse(PedidoViewModel pedido)
        {
            return new Pedido()
            {
                Id = pedido.Id,
                DataPedido = pedido.DataPedido,
                Quantidade = pedido.Quantidade,
                Desconto = pedido.Desconto,
                TaxaFrete = pedido.TaxaFrete,
                ValorTotal = pedido.ValorTotal,
                RestauranteId = pedido.RestauranteId,
                ProdutoId = pedido.ProdutoId
            };
        }
        private IEnumerable<RestauranteViewModel> getRestaurantes()
        {
            return from restaurante in
                            (Execute(() => _baseServiceRestaurante.Listar()) as OkObjectResult).Value
                            as List<Restaurante>
                   select RestaurantesController.Parse(restaurante);
        }

        private IEnumerable<ProdutoViewModel> getProdutos()
        {
            return from produto in
                            (Execute(() => _baseServiceProduto.Listar()) as OkObjectResult).Value
                            as List<Produto>
                   select ProdutosController.Parse(produto);
        }

        private double getPrecoProduto(int id)
        {
            double vlr = 0;
            try
            {
                foreach(ProdutoViewModel produto in Produtos)
                {
                    if(produto.Id == id)
                    {
                        vlr = produto.Preco;
                        break;
                    }
                }
            }
            catch
            {
                vlr = 0;
            }

            return vlr;
        }

        // GET: CidadesController
        public ActionResult Index()
        {
            var dados = from pedido in
                            (Execute(() => _baseService.Listar()) as OkObjectResult).Value
                            as List<Pedido>
                        select Parse(pedido);

            ViewBag.Restaurantes = Restaurantes;
            ViewBag.Produtos = Produtos;

            return View(dados);
        }

        // GET: CidadesController/Details/5
        public ActionResult Details(int id)
        {
            PedidoViewModel pedido = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Pedido);

            if (pedido == null)
                return NotFound();

            return View(pedido);
        }

        // GET: CidadesController/Create
        public ActionResult Create()
        {
            ViewBag.Restaurantes = Restaurantes;
            ViewBag.Produtos = Produtos;

            return View();
        }

        // POST: CidadesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id", "DataPedido", "Quantidade", "Desconto", "TaxaFrete", "ValorTotal", "RestauranteId", "ProdutoId")] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                    return NotFound();

                pedido.TaxaFrete *= 1;
                pedido.Desconto *= 1;

                pedido.ValorTotal = pedido.Quantidade * getPrecoProduto(pedido.ProdutoId) + pedido.TaxaFrete - pedido.Desconto; 

                _baseService.Inserir<PedidoValidator>(pedido);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Restaurantes = Restaurantes;
                ViewBag.Produtos = Produtos;
                ViewBag.ErroGravar = "Ocorreu um erro e os dados não foram salvos";

                return View();
            }
        }

        // GET: CidadesController/Edit/5
        public ActionResult Edit(int id)
        {
            PedidoViewModel pedido = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Pedido);

            if (pedido == null)
                return NotFound();

            ViewBag.Restaurantes = Restaurantes;
            ViewBag.Produtos = Produtos;

            return View(pedido);
        }

        // POST: CidadesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id", "DataPedido", "Quantidade", "Desconto", "TaxaFrete", "ValorTotal", "RestauranteId", "ProdutoId")] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                    return NotFound();

                pedido.TaxaFrete *= 1;
                pedido.Desconto *= 1;

                pedido.ValorTotal = pedido.Quantidade * getPrecoProduto(pedido.ProdutoId) + pedido.TaxaFrete - pedido.Desconto;

                _baseService.Alterar<PedidoValidator>(pedido);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Restaurantes = Restaurantes;
                ViewBag.Produtos = Produtos;
                ViewBag.ErroGravar = "Ocorreu um erro e os dados não foram salvos";

                return View(Parse(pedido));
            }
        }

        // GET: CidadesController/Delete/5
        public ActionResult Delete(int id)
        {
            PedidoViewModel pedido = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Pedido);

            return pedido == null ? NotFound() : View(pedido);
        }

        // POST: CidadesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [Bind("Id", "DataPedido", "Quantidade", "Desconto", "TaxaFrete", "ValorTotal", "RestauranteId", "ProdutoId")] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                    return NotFound();

                _baseService.Excluir(pedido.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                PedidoViewModel pedidoVm = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Pedido);
                ViewBag.ErroExcluir = "Erro durante a exclusão";

                return pedidoVm == null ? RedirectToAction(nameof(Index)) : View(pedidoVm);
            }
        }
    }
}
