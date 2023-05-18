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
    public class VoitureService : Service<Voiture>, IVoitureService 
    {
        public VoitureService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //Voiture la plus demandée
        public Voiture LaPlusDemandee()
        {
            return GetMany(t=>t.Couleur.Equals("Rouge")).OrderByDescending(t=>t.Courses.Count() ).FirstOrDefault();
        }
    }
}
