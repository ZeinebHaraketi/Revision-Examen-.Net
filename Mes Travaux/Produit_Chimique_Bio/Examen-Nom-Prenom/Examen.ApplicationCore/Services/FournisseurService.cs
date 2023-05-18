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
    public class FournisseurService : Service<Fournisseur>, IFournisseurService
    {
        public FournisseurService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Fournisseur> getFournissseursCategorie(Categorie categorie)
        {
            List<Produit> lp = new List<Produit>();

            List<Fournisseur> lf = lp.Where(p => p.Categorie == categorie)
                .SelectMany(f => f.Fournisseurs) 
                .Distinct()
                .ToList();  
                

         return lf;

        }
    }
}
