using Examen.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
    public class PretLivreController : Controller
    {
        private readonly IPretLivreService pretLivreService;
        private readonly IService<Abonne> abonneService;
        private readonly IService<Livre> livreService;

        public PretLivreController(IPretLivreService pretLivreService, IAbonneService abonneService, ILivreService livreService)
        {
            this.pretLivreService = pretLivreService;
            this.abonneService = abonneService;
            this.livreService = livreService;
        }
        // GET: PretLivreController
        public ActionResult Index()
        {
            return View(pretLivreService.GetAll().ToList());
        }

        [HttpPost]
        public ActionResult Index(DateTime? dateDebut, DateTime? dateFin)
        {
            IEnumerable<PretLivre> list;

            if (dateDebut != null && dateFin != null)
            {
                list = pretLivreService.GetMany(e => e.DateDebut >= dateDebut.Value && e.DateFin <= dateFin.Value).ToList();
            }
            else if (dateDebut != null)
            {
                list = pretLivreService.GetMany(e => e.DateDebut >= dateDebut.Value).ToList();
            }
            else if (dateFin != null)
            {
                list = pretLivreService.GetMany(e => e.DateFin <= dateFin.Value).ToList();
            }
            else
            {
                list = pretLivreService.GetAll().ToList();
            }

            return View(list);
        }

        // GET: PretLivreController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PretLivreController/Create
        public ActionResult Create()
        {
            ViewBag.Abonnes = new SelectList(abonneService.GetAll(), "Id", "Nom");
            ViewBag.Livres = new SelectList(livreService.GetAll(), "LivreId", "Titre");
            return View();
        }

        // POST: PretLivreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PretLivre pretLivre)
        {
            try
            {
                pretLivreService.Add(pretLivre);
                pretLivreService.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PretLivreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PretLivreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PretLivreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PretLivreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
