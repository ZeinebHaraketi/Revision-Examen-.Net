
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
    public class DonatorService : Service<Donator>, IDonatorService
    {
        public DonatorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        //implémentation des méthodes
        public IEnumerable<Donator> GetAvailableDonators()
        {
            var donators = GetAll();
            var kafalas = GetAll();

            var availableDonators = donators
                .Where(d => !kafalas.Any(k => k.Id == d.Id))
                .ToList();

            return availableDonators;
        }
    }
}

  