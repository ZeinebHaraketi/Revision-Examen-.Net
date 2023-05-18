using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class PretLivre
    {
        public DateTime DateFin { get; set; }
        public DateTime DateDebut { get; set; }

        public int LivreFk { get; set; }

        public virtual Livre Livre { get; set; }

        public int AbonneFk { get; set; }


        public virtual Abonne Abonne { get; set; }




    }
}
