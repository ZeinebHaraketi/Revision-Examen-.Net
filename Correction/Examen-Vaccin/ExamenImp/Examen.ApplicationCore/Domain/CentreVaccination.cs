using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class CentreVaccination
    {
        public int CentreVaccinationId { get; set; }

        public int Capacite { get; set; }
        public int NbChaises { get; set; }
        public int NumTelephone { get; set; }
        public string ResponsableCentre { get; set; }

        public virtual IList<Vaccin> vaccins { get; set; }
    }
}
