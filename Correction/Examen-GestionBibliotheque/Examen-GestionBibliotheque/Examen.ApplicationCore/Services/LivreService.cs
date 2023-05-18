using Examen.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class LivreService : Service<Livre>, ILivreService
    {
        public LivreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}
        public Livre GetMostPrete()
        {
            return GetAll().OrderByDescending(e => e.PretLivres.Count).FirstOrDefault();
        }
    }
}
