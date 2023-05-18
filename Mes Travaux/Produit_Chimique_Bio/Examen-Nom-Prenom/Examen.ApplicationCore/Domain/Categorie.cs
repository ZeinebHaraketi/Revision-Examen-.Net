using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Categorie
    {
        public int Id { get; set; } 
        public string Nom { get; set; }

        public virtual IList<Produit> ProduitList { get; set;}
    }
}
