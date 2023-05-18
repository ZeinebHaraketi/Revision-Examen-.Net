using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Prestataire
    {
        [Range(0, 5)]
        public int Note { get; set; }
        public string PageInstagram { get; set; }

        public int PrestataireId { get; set; }
        public string PrestataireNom { get; set; }
        public string PrestataireTel { get; set; }

        public Zone Zone { get; set; }

        public virtual IList<Prestation> Prestations { get; set; }



    }
}
