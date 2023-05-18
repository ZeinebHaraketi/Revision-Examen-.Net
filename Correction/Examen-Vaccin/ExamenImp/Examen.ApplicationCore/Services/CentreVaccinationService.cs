
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
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

        public bool Capacite(DateTime Date, CentreVaccination centre)
        {
            int TotalCitoyens = 0;
            foreach (Vaccin v in centre.vaccins)
            {
                TotalCitoyens = TotalCitoyens + v.rendezVous.ToList().Count(r => r.DateVaccination == Date);
            }
            if (centre.Capacite >= TotalCitoyens)
                return true;
            return false;
        }
    }
}
