using System;
using System.Collections.Generic;
using System.Text;

namespace Examen.ApplicationCore.Domain
{
    public class PretLivre
    {
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int AbonneFK { get; set; }
        public virtual Abonne Abonne { get; set; }
        public int LivreFK { get; set; }
        public virtual Livre Livre { get; set; }
    }
}
