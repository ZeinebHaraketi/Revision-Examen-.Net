using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Examen.Interfaces;
using Examen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class LivreService : Service<Livre>, ILivreService
    {
        private readonly IPretService _empruntService;
        private readonly ILivreService _livreService;
        private readonly List<PretLivre> _emprunts; // Remplacez par votre mécanisme de stockage des emprunts




        public LivreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Livre GetLePlusEmpruntes()
        {
            var emprunts = _empruntService.GetAll();
            var livres = _livreService.GetAll();
            
            var livresEmp = livres.OrderByDescending(l => l.PretLivres.Count)
                .FirstOrDefault();


            return livresEmp;
            
        }

        public List<Categorie> LivresEmpruntesAbonnes(Statut statut)
        {
            List<Abonne> abonnes = new List<Abonne>(); // Remplacez par votre source de données des abonnés
            List<Livre> livres = new List<Livre>(); // Remplacez par votre source de données des livres

            var abonnesAvecStatut = abonnes.Where(a => a.Statut == statut);

            List<Categorie> categoriesLivresEmpruntes = livres
                .Where(l => l.PretLivres.Any(p => abonnesAvecStatut.Contains(p.Abonne)))
                .Select(l => l.Categorie)
                .Distinct()
                .ToList();

            return categoriesLivresEmpruntes;
        }

        public List<PretLivre> ObtenirEmpruntsEntreDates(DateTime dateDebut, DateTime dateFin)
        {
            List<PretLivre> empruntsEntreDates = _emprunts
                .Where(e => e.DateDebut >= dateDebut && e.DateFin <= dateFin)
                .ToList();

            return empruntsEntreDates;
        }

        public List<Livre> ObtenirLivresEmpruntesEntreDates(DateTime dateDebut, DateTime dateFin)
        {
            List<PretLivre> empruntsEntreDates = ObtenirEmpruntsEntreDates(dateDebut, dateFin);

            List<Livre> livresEmpruntes = empruntsEntreDates
                .Select(e=> e.Livre) 
                .Distinct()
                .ToList();

            return livresEmpruntes; 

        }
    }
}
