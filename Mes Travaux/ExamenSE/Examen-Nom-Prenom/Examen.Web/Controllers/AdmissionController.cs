using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Web.Controllers
{
   
    public class AdmissionController : Controller
    {
        IAdmissionService ads { get; set; }
        IPatientService ps { get; set; }

        IChambreService cs { get; set; }
        public AdmissionController(IAdmissionService ads, IPatientService ps, IChambreService cs)
        {
            this.ads = ads;
            this.ps = ps;
            this.cs = cs;
        }

       

        // GET: AdmissionController
        public ActionResult Index()
        {
            return View(ads.GetAll().OrderByDescending(t=> t.DateAdmission) );
        }

        // GET: AdmissionController/Details/5
        public ActionResult Details(int id)
        {
            return View(ps.GetById(id));
        }

        // GET: AdmissionController/Create
        public ActionResult Create()
        {
            ViewBag.ChambreFK =
                new SelectList(cs.GetAll(), "NumeroChambre", "NumeroChambre");

            ViewBag.PatientFk =
               new SelectList(ps.GetAll(), "NumDossier", "CIN");

            return View();
        }

        // POST: AdmissionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admission admission)
        {
            try
            {
                ads.Add(admission);
                ads.Commit();
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
