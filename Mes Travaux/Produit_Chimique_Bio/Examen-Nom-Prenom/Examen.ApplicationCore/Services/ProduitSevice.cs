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
    public class ProduitSevice : Service<Produit>, IProduitService
    {
        public ProduitSevice(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public double getMoyPrixProduit(Categorie categorie)
        {
            //retourner la moyenne des prix des produits biologiques d’une catégorie donnée.


            //return GetAll().OfType<Biologique>().Where(p => p.Categorie.Equals(categorie))
            //    .Average(p => p.Price);

            return GetMany(p => p.Categorie.Equals(categorie)).OfType<Biologique>()
                .Average(p => p.Price);
        }

        public IEnumerable<Chimique> getProduitPrices()
        {

            return GetAll().OfType<Chimique>()
                .OrderByDescending(c => c.Price).Take(5);    
                

        }
    }
}
