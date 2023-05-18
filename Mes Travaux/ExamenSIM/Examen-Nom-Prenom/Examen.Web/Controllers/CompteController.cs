using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Web.Controllers
{
    public class CompteController : Controller
    {
      
        ICompteService cs { get; set; }
        IBanqueService bs { get; set; }

        public CompteController(ICompteService cs, IBanqueService bs)
        {
            this.cs = cs;
            this.bs = bs;
        }

        // GET: CompteController
        public ActionResult Index(TypeCompte? tp)
        {
            if (tp == null)
            {
                return View(cs.GetAll());
            }
            return View(cs.GetMany(p => p.Type.Equals(tp)));   
        }

        // GET: CompteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompteController/Create
        public ActionResult Create()
        {
            ViewBag.BanqueFK = new SelectList(bs.GetAll(), "Code", "Nom");
            return View();
        }

        // POST: CompteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Compte compte)
        {
            try
            {
                cs.Add(compte);
                cs.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompteController/Edit/5
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

        // GET: CompteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompteController/Delete/5
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
