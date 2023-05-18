using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Examen.ApplicationCore.Domain
{
    public class Categorie
    {
        [Key]
        public int Code { get; set; }
        public string Libelle { get; set; }
        public virtual IList<Livre> Livres { get; set; }
    }
}
