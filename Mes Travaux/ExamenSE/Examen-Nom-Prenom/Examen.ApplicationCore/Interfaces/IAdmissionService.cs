using Examen.ApplicationCore.Domain;
using Examen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IAdmissionService : IService<Admission>
    {
        public IEnumerable<NomComplet> getOccupant(Chambre c, DateTime date);

        public double RecetteClinique(Clinique c, int annee);
    }
}
