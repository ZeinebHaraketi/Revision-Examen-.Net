using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen.Web.Controllers
{
    public class BanqueController : Controller
    {
        IBanqueService bs { get; set; }
        public BanqueController(IBanqueService bs)
        {
            this.bs = bs;
        }

        // GET: BanqueController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BanqueController/Details/5
        public ActionResult Details(int id)
        {
            return View(bs.GetById(id));
        }

        // GET: BanqueController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BanqueController/Create
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

        // GET: BanqueController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(bs.GetById(id));
        }

        // POST: BanqueController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Banque banque)
        {
            try
            {
                bs.Update(banque);
                bs.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BanqueController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BanqueController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Banque banque)
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
