using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class PretLivreService : Service<PretLivre>, IPretLivreService
    {
        public PretLivreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}
        public IEnumerable<Livre> GetLivresPretes(DateTime debut, DateTime fin)
        {
            return GetMany(e => e.DateDebut >= debut && e.DateFin <= fin).Select(e => e.Livre);
        }
        public IEnumerable<Categorie> GetCategoriesLivresPretes(Statut statut)
        {
            return GetMany(e => e.Abonne.Statut == statut).
                Select(e => e.Livre.Categorie).GroupBy(e => e.Code).Select(e => e.First());
        }
    }
}
