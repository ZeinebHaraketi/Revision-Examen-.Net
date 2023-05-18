
using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examen.ApplicationCore.Interfaces
{
    public interface ICentreVaccinationService:IService<CentreVaccination>
    {
        public bool Capacite(DateTime Date, CentreVaccination centre);
    }
   
}
