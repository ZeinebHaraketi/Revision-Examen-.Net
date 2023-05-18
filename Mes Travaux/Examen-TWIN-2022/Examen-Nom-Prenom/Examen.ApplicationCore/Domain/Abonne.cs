using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Abonne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string Adresse { get; set; }


        public string Email { get; set; }


        public DateTime DateCreation { get; set; }

        public Statut Statut { get; set; }

        public virtual IList<PretLivre> PretLivres { get; set; }

    }
}
