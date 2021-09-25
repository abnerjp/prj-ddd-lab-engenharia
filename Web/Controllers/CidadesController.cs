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
    public class CidadesController : Controller
    {
        private IBaseService<Cidade> _baseService;


        public CidadesController(IBaseService<Cidade> baseService)
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

        private CidadeViewModel Parse(Cidade cidade)
        {
            return new CidadeViewModel()
            {
                Id = cidade.Id,
                Nome = cidade.Nome,
                Descricao = cidade.Descricao,
                PaisId = cidade.PaisId
            };
        }

        private Cidade Parse(CidadeViewModel cidade)
        {
            return new Cidade()
            {
                Id = cidade.Id,
                Nome = cidade.Nome,
                Descricao = cidade.Descricao,
                PaisId = cidade.PaisId
            };
        }

        // GET: CidadesController
        public ActionResult Index()
        {
            var dados = from cidade in
                            (Execute(() => _baseService.Listar()) as OkObjectResult).Value
                            as List<Cidade>
                        select Parse(cidade);

            return View(dados);
        }

        // GET: CidadesController/Details/5
        public ActionResult Details(int id)
        {
            CidadeViewModel cidade = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Cidade);

            if (cidade == null)
                return NotFound();

            return View();
        }

        // GET: CidadesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CidadesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Nome", "Descricao", "PaisId")] Cidade cidade)
        {
            try
            {
                if (cidade == null)
                    return NotFound();

                _baseService.Inserir<CidadeValidator>(cidade);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CidadesController/Edit/5
        public ActionResult Edit(int id)
        {
            CidadeViewModel cidade = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Cidade);

            if (cidade == null)
                return NotFound();

            return View(cidade);
        }

        // POST: CidadesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id", "Nome", "Descricao", "PaisId")] Cidade cidade)
        {
            try
            {
                if (cidade == null)
                    return NotFound();

                _baseService.Alterar<CidadeValidator>(cidade);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(Parse(cidade));
            }
        }

        // GET: CidadesController/Delete/5
        public ActionResult Delete(int id)
        {
            CidadeViewModel cidade = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Cidade);

            if (cidade == null)
                return NotFound();

            return View(cidade);
        }

        // POST: CidadesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [Bind("Id", "Nome", "Descricao", "PaisId")] Cidade cidade)
        {
            try
            {
                if (cidade == null)
                    return NotFound();

                _baseService.Excluir(cidade.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(Parse(cidade));
            }
        }
    }
}
