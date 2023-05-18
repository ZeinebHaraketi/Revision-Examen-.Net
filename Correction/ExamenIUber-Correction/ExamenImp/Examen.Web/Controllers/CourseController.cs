using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen.Web.Controllers
{
    public class CourseController : Controller
    {
        IServiceCourse serviceCourse;
        IServiceClient serviceClient;
        IServiceVoiture serviceVoiture;
        IServiceChauffeur serviceChauffeur;

        public CourseController(IServiceCourse serviceCourse, IServiceClient serviceClient, IServiceVoiture serviceVoiture, IServiceChauffeur serviceChauffeur)
        {
            this.serviceCourse = serviceCourse;
            this.serviceClient = serviceClient;
            this.serviceVoiture = serviceVoiture;
            this.serviceChauffeur = serviceChauffeur;
        }




        // GET: CourseController
        public ActionResult Index()
        {
            return View(serviceCourse.GetAll());
        }
        public ActionResult ListByChauffeur(int id)
        {
            return View(serviceCourse.GetPayedCourses(serviceChauffeur.GetById(id), DateTime.Now));
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            ViewBag.Clients = new SelectList(serviceClient.GetAll(), "CIN", "CIN");
            ViewBag.Voitures = new SelectList(serviceVoiture.GetAll(), "NumMat", "NumMat");

            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
         
            
                serviceCourse.Add(course);
                serviceCourse.Commit();
                return RedirectToAction(nameof(Index));
         
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseController/Edit/5
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

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseController/Delete/5
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
