using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public  class Produit
    {
        [DataType(DataType.DateTime, ErrorMessage = "Date invalid")]
        public DateTime DateProd { get; set; }

        public string Destination { get; set; }

        public string Nom { get; set; }

        public double Price { get; set; }

        public int ProduitId { get; set; }

        public virtual IList<Fournisseur> Fournisseurs { get; set; }

        public int CategorieFK { get; set; }

        [ForeignKey("CategorieFK")]
        public virtual Categorie Categorie { get; set; }

    }
}
