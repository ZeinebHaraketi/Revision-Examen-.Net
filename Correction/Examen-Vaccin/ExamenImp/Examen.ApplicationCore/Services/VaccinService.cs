
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class VaccinService : Service<Vaccin>, IVaccinService
    {
        public VaccinService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public string Validite(Vaccin v)
        {
            if (v.DateValidite >= DateTime.Now && v.Quantite > 0)
                return "Valide";
            else return "Invalide";
        }
    }
}
