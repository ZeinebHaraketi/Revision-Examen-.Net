using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Web.Controllers
{
    public class AdmissionController : Controller
    {
        IServiceChambre sc;
        IServicePatient sp;
        IServiceAdmission sa;

        public AdmissionController(IServiceChambre sc, IServicePatient sp, IServiceAdmission sa)
        {
            this.sc = sc;
            this.sp = sp;
            this.sa = sa;
        }

        // GET: AdmissionController
        public ActionResult Index()
        {
            return View(sa.GetAll().OrderBy(p=>p.DateAdmission));
        }

        // GET: AdmissionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdmissionController/Create
        public ActionResult Create()
        {
            ViewBag.PatienList = new SelectList(sp.GetAll(), "CIN", "NumDossier");
            ViewBag.ChambreList = new SelectList(sc.GetAll(), "NumeroChambre", "Prix");
            return View();
        }

        // POST: AdmissionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admission admission)
        {
            try
            {
                sa.Add(admission);
                sa.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdmissionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdmissionController/Edit/5
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

        // GET: AdmissionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdmissionController/Delete/5
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
