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
    public class VaccinService : Service<Vaccin>, IVaccinService
    {
        public VaccinService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public string ValidOuPas()
        {
            var test = true;
            var ch = "";

            if (GetMany(t => (DateTime.Now - t.DateValidite).TotalDays < 0 && t.Quantite > 0)
                .Equals(test)){

                ch = "valide";
            }
            ch = "Non Valide";

 
            return ch;
              

        }
    }
}
