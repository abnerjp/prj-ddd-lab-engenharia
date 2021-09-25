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
    public class CozinhasController : Controller
    {
        private IBaseService<Cozinha> _baseService;


        public CozinhasController(IBaseService<Cozinha> baseService)
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

        private CozinhaViewModel Parse(Cozinha cozinha)
        {
            return new CozinhaViewModel()
            {
                Id = cozinha.Id,
                Nome = cozinha.Nome,
                Observacao = cozinha.Observacao
            };
        }

        private Cozinha Parse(CozinhaViewModel cozinha)
        {
            return new Cozinha()
            {
                Id = cozinha.Id,
                Nome = cozinha.Nome,
                Observacao = cozinha.Observacao
            };
        }

        // GET: PaisesController
        public ActionResult Index()
        {
            var dados = from cozinha in
                            (Execute(() => _baseService.Listar()) as OkObjectResult).Value
                            as List<Cozinha>
                            select Parse(cozinha);

            return View(dados);
        }

        // GET: PaisesController/Details/5
        public ActionResult Details(int id)
        {
            CozinhaViewModel cozinha = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Cozinha);

            if (cozinha == null)
                return NotFound();

            return View(cozinha);
        }

        // GET: PaisesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaisesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Nome", "Observacao")] Cozinha cozinha)
        {
            try
            {
                if (cozinha == null)
                    return NotFound();

                _baseService.Inserir<CozinhaValidator>(cozinha);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaisesController/Edit/5
        public ActionResult Edit(int id)
        {
            CozinhaViewModel cozinha = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Cozinha);

            if (cozinha == null)
                return NotFound();

            return View(cozinha);
        }

        // POST: PaisesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id", "Nome", "Observacao")] Cozinha cozinha)
        {
            try
            {
                if (cozinha == null)
                    return NotFound();

                _baseService.Alterar<CozinhaValidator>(cozinha);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(Parse(cozinha));
            }
        }

        // GET: PaisesController/Delete/5
        public ActionResult Delete(int id)
        {
            CozinhaViewModel cozinha = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Cozinha);

            if (cozinha == null)
                return NotFound();

            return View(cozinha);
        }

        // POST: PaisesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [Bind("Id", "Nome", "Observacao")] Cozinha pais)
        {
            try
            {
                if (pais == null)
                    return NotFound();

                _baseService.Excluir(pais.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(Parse(pais));
            }
        }
    }
}
