using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Web.Controllers
{
    public class VaccinController : Controller
    {
        ICentreVaccinationService cs { get; set; }
        IVaccinService vs { get; set; }
        public VaccinController(ICentreVaccinationService cs, IVaccinService vs)
        {
            this.cs = cs;
            this.vs = vs;
        }

       
        // GET: VaccinController
        public ActionResult Index()
        {
            return View(vs.GetAll().OrderByDescending(t=> t.DateValidite));
        }

        // GET: VaccinController/Details/5
        public ActionResult Details(int id)
        {
            return View( );
        }

        // GET: VaccinController/Create
        public ActionResult Create()
        {
            ViewBag.CentreVaccinationId =
                new SelectList(cs.GetAll(), "CentreVaccinationId", "CentreVaccinationId");
            return View();
        }

        // POST: VaccinController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vaccin vaccin)
        {
            try
            {
                vs.Add(vaccin);
                vs.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VaccinController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VaccinController/Edit/5
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

        // GET: VaccinController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VaccinController/Delete/5
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
