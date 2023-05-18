using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Clinique
    {
        public string Adresse { get; set; }
        public int Capacite { get; set; }

        [Key]
        public int CliniqueId { get; set; }

        public int NumTel { get; set; }


        public virtual IList<Chambre> Chambres { get; set; }


    }
}
