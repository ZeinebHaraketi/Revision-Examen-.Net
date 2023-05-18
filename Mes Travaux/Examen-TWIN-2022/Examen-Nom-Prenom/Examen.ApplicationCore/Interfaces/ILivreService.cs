using Examen.ApplicationCore.Domain;
using Examen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Interfaces
{
    public interface ILivreService: IService<Livre>
    {
        public Livre GetLePlusEmpruntes();

        List<Livre> ObtenirLivresEmpruntesEntreDates(DateTime dateDebut, DateTime dateFin);

        List<Categorie> LivresEmpruntesAbonnes(Statut statut);
    }
}
