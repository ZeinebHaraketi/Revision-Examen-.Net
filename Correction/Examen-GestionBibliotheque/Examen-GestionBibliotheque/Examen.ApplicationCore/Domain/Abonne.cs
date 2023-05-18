using System;
using System.Collections.Generic;
using System.Text;

namespace Examen.ApplicationCore.Domain
{
    public class Abonne
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Id { get; set; }
        public Statut Statut { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public DateTime DateCreation { get; set; }
        public virtual Categorie Categorie { get; set; }
        public virtual IList<PretLivre> PretLivres { get; set; }
    }
}
