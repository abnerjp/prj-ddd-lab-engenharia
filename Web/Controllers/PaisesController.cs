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
    public class PaisesController : Controller
    {
        private IBaseService<Pais> _baseService;


        public PaisesController(IBaseService<Pais> baseService)
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

        private PaisViewModel Parse(Pais pais)
        {
            return new PaisViewModel()
            {
                Id = pais.Id,
                Nome = pais.Nome,
                Sigla = pais.Sigla
            };
        }

        private Pais Parse(PaisViewModel pais)
        {
            return new Pais()
            {
                Id = pais.Id,
                Nome = pais.Nome,
                Sigla = pais.Sigla
            };
        }

        // GET: PaisesController
        public ActionResult Index()
        {
            var dados = from pais in
                            (Execute(() => _baseService.Listar()) as OkObjectResult).Value
                            as List<Pais>
                            select Parse(pais);

            return View(dados);
        }

        // GET: PaisesController/Details/5
        public ActionResult Details(int id)
        {
            PaisViewModel pais = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Pais);

            if (pais == null)
                return NotFound();

            return View(pais);
        }

        // GET: PaisesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaisesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Nome", "Sigla")] Pais pais)
        {
            try
            {
                if (pais == null)
                    return NotFound();

                _baseService.Inserir<PaisValidator>(pais);

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
            PaisViewModel pais = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Pais);

            if (pais == null)
                return NotFound();

            return View(pais);
        }

        // POST: PaisesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id", "Nome", "Sigla")] Pais pais)
        {
            try
            {
                if (pais == null)
                    return NotFound();

                _baseService.Alterar<PaisValidator>(pais);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(Parse(pais));
            }
        }

        // GET: PaisesController/Delete/5
        public ActionResult Delete(int id)
        {
            PaisViewModel pais = Parse((Execute(() => _baseService.ListarPorId(id)) as OkObjectResult).Value as Pais);

            if (pais == null)
                return NotFound();

            return View(pais);
        }

        // POST: PaisesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [Bind("Id", "Nome", "Sigla")] Pais pais)
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
