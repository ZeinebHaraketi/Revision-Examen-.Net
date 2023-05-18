using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IPretLivreService : IService<PretLivre>
    {
        IEnumerable<Livre> GetLivresPretes(DateTime debut, DateTime fin);
        IEnumerable<Categorie> GetCategoriesLivresPretes(Statut statut);
    }
}
