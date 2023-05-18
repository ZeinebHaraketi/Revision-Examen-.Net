using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Web.Controllers
{
    public class EmpruntController : Controller
    {

        IPretService ps;
        IAbonneService ias;
        ILivreService ls;

        public EmpruntController(IPretService ps, IAbonneService ias, ILivreService ls)
        {
            this.ps = ps;
            this.ias = ias;
            this.ls = ls;
        }


        // GET: PretLivreController
        public ActionResult Index(DateTime? dd, DateTime? df)
        {
            if (dd == null || df== null)
            {
                return View(ps.GetAll());

            }
            return View(ps.GetMany(p => p.DateDebut.Equals(dd) || p.DateFin.Equals(df)) );

        }

        // GET: PretLivreController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PretLivreController/Create
        public ActionResult Create()
        {
            ViewBag.AbonneFk = new SelectList( ias.GetAll(), "Id", "Nom" );
            ViewBag.LivreFk = new SelectList( ls.GetAll(), "Id", "Titre" );
            return View();
        }

        // POST: PretLivreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PretLivre pret)
        {
            try
            {
                ps.Add(pret);
                ps.Commit();
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
