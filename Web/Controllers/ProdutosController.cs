using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class ProdutosController : Controller
    {
        private IBaseService<Produto> _baseService;


        public ProdutosController(IBaseService<Produto> baseService)
        {
            _baseService = baseService;
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

        public static ProdutoViewModel Parse(Produto produto)
        {
            return new ProdutoViewModel()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco
            };
        }

        public static Produto Parse(ProdutoViewModel produto)
        {
            return new Produto()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco
            };
        }

        // GET: CidadesController
        public ActionResult Index()
        {
            var dados = from produto in
                            (Execute(() => _baseService.Listar()) as OkObjectResult).Value
                            as List<Produto>
                        select Parse(produto);

            return View(dados);
        }

        // GET: CidadesController/Details/5
        public ActionResult Details(int id)
        {
            ProdutoViewModel produto = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Produto);

            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // GET: CidadesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CidadesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Nome", "Descricao", "Preco")] Produto produto)
        {
            try
            {
                if (produto == null)
                    return NotFound();

                _baseService.Inserir<ProdutoValidator>(produto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ErroGravar = "Ocorreu um erro e os dados não foram salvos";

                return View();
            }
        }

        // GET: CidadesController/Edit/5
        public ActionResult Edit(int id)
        {
            ProdutoViewModel produto = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Produto);

            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // POST: CidadesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id", "Nome", "Descricao", "Preco")] Produto produto)
        {
            try
            {
                if (produto == null)
                    return NotFound();

                _baseService.Alterar<ProdutoValidator>(produto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ErroGravar = "Ocorreu um erro e os dados não foram salvos";

                return View(Parse(produto));
            }
        }

        // GET: CidadesController/Delete/5
        public ActionResult Delete(int id)
        {
            ProdutoViewModel produto = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Produto);

            return produto == null ? NotFound() : View(produto);
        }

        // POST: CidadesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [Bind("Id", "Nome", "Descricao", "Preco")] Produto produto)
        {
            try
            {
                if (produto == null)
                    return NotFound();

                _baseService.Excluir(produto.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ProdutoViewModel produtoVm = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Produto);
                ViewBag.ErroExcluir = "Erro durante a exclusão";

                return produtoVm == null ? RedirectToAction(nameof(Index)) : View(produtoVm);
            }
        }
    }
}
