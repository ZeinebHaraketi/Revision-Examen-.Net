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
    public class CentreVaccinationService : Service<CentreVaccination>, ICentreVaccinationService
    {
        public CentreVaccinationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool verifCentre(DateTime dateV, CentreVaccination c)
        {
            int TotalCitoyens = 0;

            foreach (Vaccin v in c.Vaccins)
            {
                TotalCitoyens = TotalCitoyens + v.RendezVous.ToList().Count(t => t.DateVaccination == dateV);
            }
            if (c.Capacite>TotalCitoyens)
            {
                return true;
            }
            return false;
           
        }
    }
}
