using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class ServiceChauffeur : Service<Chauffeur>, IServiceChauffeur
    {
        public ServiceChauffeur(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

     
    }
}
