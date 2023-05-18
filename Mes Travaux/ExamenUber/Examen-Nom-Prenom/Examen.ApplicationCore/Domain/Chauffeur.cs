using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Chauffeur
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public float TauxBenefice { get; set; }

        public int VoitureFK { get; set; }
        public virtual Voiture Voiture { get; set; }

    }
}
