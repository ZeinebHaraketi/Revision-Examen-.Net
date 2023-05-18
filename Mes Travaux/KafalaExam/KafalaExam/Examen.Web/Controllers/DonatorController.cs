using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen.Web.Controllers
{
    public class DonatorController : Controller
    {
        public IDonatorService ds;

           public DonatorController (IDonatorService ds)
        {
                this.ds = ds;
        }
        // GET: DonatorController
        public ActionResult Index()
        {
            return View(ds.GetAll());
        }

        // GET: DonatorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DonatorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonatorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: DonatorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DonatorController/Edit/5
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

        // GET: DonatorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DonatorController/Delete/5
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
