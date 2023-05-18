using Examen.ApplicationCore.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examen.ApplicationCore.Interfaces
{
    public interface ICitoyenService : IService<Citoyen>
    {
        public IGrouping<string, IEnumerable<Citoyen>> GetCitoyensVaccinés();
    }
  
}
