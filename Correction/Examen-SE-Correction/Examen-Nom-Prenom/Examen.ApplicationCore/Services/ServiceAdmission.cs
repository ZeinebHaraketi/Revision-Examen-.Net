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
    public class ServiceAdmission : Service<Admission>, IServiceAdmission
    {
        public ServiceAdmission(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<NomComplet> Occupants(Chambre c, DateTime debut)
        {
            return c.Admissions.Where(p => DateTime.Compare(p.DateAdmission, debut) > 0)
                .Select(p => p.Patient.NomComplet);
        }

        public double RecetteClinique(Clinique c, int annee)
        {
            return GetMany(p => p.DateAdmission.Year == annee && p.Chambre.CliniqueFk == c.CliniqueId)
                .Sum(p => p.Chambre.Prix * p.NbJours);


        }
    }
}