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
    public class CitoyenService : Service<Citoyen>, ICitoyenService
    {
        public CitoyenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IGrouping<string, IEnumerable<Citoyen>> GetCitoyensVaccines()
        {
            return (IGrouping<string, IEnumerable<Citoyen>>)
                GetMany(c => c.RendezVous
            .Count >= 0)
                .GroupBy(c => c.Adresse.Ville);

        }
    }
}
