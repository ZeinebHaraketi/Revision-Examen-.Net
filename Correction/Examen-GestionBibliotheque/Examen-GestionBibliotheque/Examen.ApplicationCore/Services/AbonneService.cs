using Examen.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class AbonneService : Service<Abonne>, IAbonneService
    {
        public AbonneService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}
        
    }
}
