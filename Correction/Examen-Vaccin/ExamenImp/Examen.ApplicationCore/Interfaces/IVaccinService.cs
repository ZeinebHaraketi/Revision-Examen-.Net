
using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IVaccinService:IService<Vaccin>
    {
        public string Validite(Vaccin v);
    }
   
}
