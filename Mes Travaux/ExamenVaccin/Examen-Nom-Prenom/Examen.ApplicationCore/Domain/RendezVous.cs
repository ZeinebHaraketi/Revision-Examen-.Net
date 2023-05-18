using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class RendezVous
    {
        public string CodeInfermiere { get; set; }

        public DateTime DateVaccination { get; set; }
        public int NbrDoses { get; set; }

        public string CitoyenId { get; set; }
        public int VaccinId { get; set; }
        public virtual Citoyen Citoyen { get; set; }
        public virtual Vaccin Vaccin { get; set; }

    }
}
