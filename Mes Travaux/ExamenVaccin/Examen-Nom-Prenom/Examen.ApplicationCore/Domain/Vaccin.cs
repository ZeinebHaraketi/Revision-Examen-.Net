using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Examen.ApplicationCore.Domain
{
    public class Vaccin
    {
        [DataType(DataType.Date)]
        public DateTime DateValidite { get; set; }
        public string Fournisseur { get; set; }
        public int Quantite { get; set; }

        public TypeVaccin TypeVaccin { get; set; }

        public int VaccinId { get; set; }

        public virtual IList<RendezVous> RendezVous { get; set; }
        public int CentreVaccinationId { get; set; }

        public virtual CentreVaccination CentreVaccination { get; set; }


    }
}
