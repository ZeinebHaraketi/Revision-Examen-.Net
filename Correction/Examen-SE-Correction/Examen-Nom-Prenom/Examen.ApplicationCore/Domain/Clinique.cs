using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Clinique
    {
        public int Capacite { get; set; }
        public int CliniqueId { get; set; }
        public int NumTel { get; set; }
        public string Adresse { get; set; }
        public virtual IList<Chambre> Chambres { get; set; }
    }
}
