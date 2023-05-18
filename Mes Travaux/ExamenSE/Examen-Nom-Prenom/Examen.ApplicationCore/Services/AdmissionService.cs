using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Examen.Interfaces;
using Examen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class AdmissionService : Service<Admission>, IAdmissionService
    {
        public AdmissionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<NomComplet> getOccupant(Chambre c, DateTime date)
        {
            return c.Admissions.Where(t => DateTime.Compare(t.DateAdmission, date) > 0)
                .Select(t => t.Patient.NomComplet);
                
        }

        public double RecetteClinique(Clinique c, int annee)
        {
            return GetMany(t => t.DateAdmission.Year == annee && t.Chambre.CliniqueFK == c.CliniqueId)
                .Sum(t=> t.Chambre.Prix * t.NbJours);
        }
    }
}
