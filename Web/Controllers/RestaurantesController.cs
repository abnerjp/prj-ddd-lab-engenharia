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
    public class RestaurantesController : Controller
    {
        private IBaseService<Restaurante> _baseService;
        private IBaseService<Cidade> _baseServiceCidade;
        private IBaseService<Cozinha> _baseServiceCozinha;

        private IEnumerable<CidadeViewModel> Cidades;
        private IEnumerable<CozinhaViewModel> Cozinhas;


        public RestaurantesController(IBaseService<Restaurante> baseService, IBaseService<Cidade> baseServiceCidade, IBaseService<Cozinha> baseServiceCozinha)
        {
            _baseService = baseService;
            _baseServiceCidade = baseServiceCidade;
            _baseServiceCozinha = baseServiceCozinha;

            this.Cidades = this.getCidades();
            this.Cozinhas = this.getCozinhas();
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

        public static RestauranteViewModel Parse(Restaurante restaurante)
        {
            return new RestauranteViewModel()
            {
                Id = restaurante.Id,
                Nome = restaurante.Nome,
                Cep = restaurante.Cep,
                Bairro = restaurante.Bairro,
                Logradouro = restaurante.Logradouro,
                Numero = restaurante.Numero,
                CozinhaId = restaurante.CozinhaId,
                CidadeId = restaurante.CidadeId
            };
        }

        public static Restaurante Parse(RestauranteViewModel restaurante)
        {
            return new Restaurante()
            {
                Id = restaurante.Id,
                Nome = restaurante.Nome,
                Cep = restaurante.Cep,
                Bairro = restaurante.Bairro,
                Logradouro = restaurante.Logradouro,
                Numero = restaurante.Numero,
                CozinhaId = restaurante.CozinhaId,
                CidadeId = restaurante.CidadeId
            };
        }
        private IEnumerable<CidadeViewModel> getCidades()
        {
            return from cidade in
                            (Execute(() => _baseServiceCidade.Listar()) as OkObjectResult).Value
                            as List<Cidade>
                   select CidadesController.Parse(cidade);
        }

        private IEnumerable<CozinhaViewModel> getCozinhas()
        {
            return from cozinha in
                            (Execute(() => _baseServiceCozinha.Listar()) as OkObjectResult).Value
                            as List<Cozinha>
                   select CozinhasController.Parse(cozinha);
        }

        // GET: CidadesController
        public ActionResult Index()
        {
            var dados = from restaurante in
                            (Execute(() => _baseService.Listar()) as OkObjectResult).Value
                            as List<Restaurante>
                        select Parse(restaurante);

            ViewBag.Cidades = Cidades;
            ViewBag.Cozinhas = Cozinhas;

            return View(dados);
        }

        // GET: CidadesController/Details/5
        public ActionResult Details(int id)
        {
            RestauranteViewModel restaurante = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Restaurante);

            if (restaurante == null)
                return NotFound();

            return View(restaurante);
        }

        // GET: CidadesController/Create
        public ActionResult Create()
        {
            ViewBag.Cidades = Cidades;
            ViewBag.Cozinhas = Cozinhas;

            return View();
        }

        // POST: CidadesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Nome", "Cep", "Bairro", "Logradouro", "Numero", "CozinhaId", "CidadeId")] Restaurante restaurante)
        {
            try
            {
                if (restaurante == null)
                    return NotFound();

                _baseService.Inserir<RestauranteValidator>(restaurante);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Cidades = Cidades;
                ViewBag.Cozinhas = Cozinhas;
                ViewBag.ErroGravar = "Ocorreu um erro e os dados não foram salvos";

                return View();
            }
        }

        // GET: CidadesController/Edit/5
        public ActionResult Edit(int id)
        {
            RestauranteViewModel restaurante = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Restaurante);

            if (restaurante == null)
                return NotFound();

            ViewBag.Cidades = Cidades;
            ViewBag.Cozinhas = Cozinhas;

            return View(restaurante);
        }

        // POST: CidadesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id", "Nome", "Cep", "Bairro", "Logradouro", "Numero", "CozinhaId", "CidadeId")] Restaurante restaurante)
        {
            try
            {
                if (restaurante == null)
                    return NotFound();

                _baseService.Alterar<RestauranteValidator>(restaurante);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Cidades = Cidades;
                ViewBag.Cozinhas = Cozinhas;
                ViewBag.ErroGravar = "Ocorreu um erro e os dados não foram salvos";

                return View(Parse(restaurante));
            }
        }

        // GET: CidadesController/Delete/5
        public ActionResult Delete(int id)
        {
            RestauranteViewModel restaurante = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Restaurante);

            return restaurante == null ? NotFound() : View(restaurante);
        }

        // POST: CidadesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [Bind("Id", "Nome", "Cep", "Bairro", "Logradouro", "Numero", "CozinhaId", "CidadeId")] Restaurante restaurante)
        {
            try
            {
                if (restaurante == null)
                    return NotFound();

                _baseService.Excluir(restaurante.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                RestauranteViewModel restauranteVm = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Restaurante);
                ViewBag.ErroExcluir = "Erro durante a exclusão";

                return restauranteVm == null ? RedirectToAction(nameof(Index)) : View(restauranteVm);
            }
        }
    }
}
