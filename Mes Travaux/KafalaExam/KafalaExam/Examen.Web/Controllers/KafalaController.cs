using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Web.Controllers
{
    public class KafalaController : Controller
    {
        public IKafalaService kfs;
        public IBeneficiaryService bs;
        public IDonatorService ds;
        public KafalaController(IKafalaService kfs, IBeneficiaryService bs , IDonatorService ds) {
         this.kfs = kfs;
         this.bs = bs;  
         this.ds = ds;
        }
        // GET: KafalaController
        public ActionResult Index()
        {
            return View(kfs.GetAll());
        }

        // GET: KafalaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: KafalaController/Create
        public ActionResult Create()
        {
            ViewBag.BenfList = new SelectList(bs.GetAll(), "CIN", "Name");
            ViewBag.DonList = new SelectList(ds.GetAll(), "Id", "Name");

            return View();
        }

        // POST: KafalaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kafala kafala)
        {
            try
            {
                kfs.Add(kafala);
                kfs.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KafalaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: KafalaController/Edit/5
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

        // GET: KafalaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KafalaController/Delete/5
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
