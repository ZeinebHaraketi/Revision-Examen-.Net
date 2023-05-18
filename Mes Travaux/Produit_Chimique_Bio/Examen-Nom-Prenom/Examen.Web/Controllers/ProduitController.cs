using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Web.Controllers
{
    public class ProduitController : Controller
    {
       

        IProduitService ps { get; set; }
        ICategorieService cs { get; set; }


        public ProduitController(IProduitService ps, ICategorieService cs)
        {
            this.ps = ps;
            this.cs = cs;
        }
        // GET: ProduitController
        public ActionResult Index(string? nom)
        {
            if (nom == null)
            {
                return View(ps.GetAll());
            }
            return View(ps.GetMany(p=> p.Nom.Equals(nom)));
        }

        // GET: ProduitController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProduitController/Create
        public ActionResult Create()
        {
            ViewBag.CategorieFK = new SelectList(cs.GetAll(), "Id", "Nom");
            return View();
        }

        // POST: ProduitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produit produit)
        {
            try
            {
                ps.Add(produit);
                ps.Commit();  
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProduitController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProduitController/Edit/5
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

        // GET: ProduitController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProduitController/Delete/5
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
