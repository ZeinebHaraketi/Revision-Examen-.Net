using Examen.ApplicationCore.Domain;
using Examen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IPrestationService : IService<Prestation>
    {
        public IEnumerable<Prestataire> threePres(Prestataire p);
        public double getPrixMoyen();


    }
}
