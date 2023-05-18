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
    public class ServiceChambre : Service<Chambre>, IServiceChambre
    {
        public ServiceChambre(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public double PourcentageChambre(Clinique c)
        {
            //return GetMany(p => p.CliniqueFk == c.CliniqueId && p.TypeChambre == TypeChambre.simple).Count()
            //     / GetMany(p => p.CliniqueFk.Equals(c.CliniqueId)).Count() * 100;

            return c.Chambres.Where(p => p.TypeChambre == TypeChambre.simple).Count()
                / c.Chambres.Count() * 100;

        }
    }
}
