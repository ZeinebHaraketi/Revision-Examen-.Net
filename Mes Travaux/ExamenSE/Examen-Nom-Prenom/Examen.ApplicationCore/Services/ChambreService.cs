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
    public class ChambreService : Service<Chambre>, IChambreService
    {
        public ChambreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public float GetPourcentagesSimples(Clinique clinique)
        {
            return clinique.Chambres.Where(t => t.TypeChambre == TypeChambre.Simple).Count()
                / clinique.Chambres.Count()*100;
                
        }
    }
}
