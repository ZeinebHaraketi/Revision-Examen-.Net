using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Web.Controllers
{
    public class VaccinController : Controller
    {
        readonly IVaccinService vaccinService;
        readonly ICentreVaccinationService centreVaccination;
        public VaccinController(IVaccinService vaccinService, ICentreVaccinationService centreVaccination)
        {
            this.vaccinService = vaccinService;
            this.centreVaccination = centreVaccination;
        }
        // GET: VaccinController
        public ActionResult Index()
        {
            var vaccins = vaccinService.GetAll().ToList().OrderByDescending(v => v.DateValidite);

            foreach (var item in vaccins)
            {

               
                item.Validité = vaccinService.Validite(item);
            }

            return View(vaccins);
        }

        // GET: VaccinController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VaccinController/Create
        public ActionResult Create()
        {
            ViewBag.vaccinations = new SelectList(centreVaccination.GetAll(), "CentreVaccinationId", "ResponsableCentre");
            return View();
        }


        // POST: VaccinController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vaccin vac)
        {
            try
            {
                vaccinService.Add(vac);
                vaccinService.Commit();
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
