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
    public class ServiceCourse : Service<Course>, IServiceCourse
    {
        public ServiceCourse(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public double GetBenefice(Chauffeur chauffeur, DateTime date)
        {
            return GetPayedCourses(chauffeur, date).Sum(e => e.Montant * chauffeur.TauxBenefice);
        }

        public IEnumerable<Course> GetPayedCourses(Chauffeur chauffeur, DateTime date)
        {
            return GetMany(e => e.Etat == Etat.Payee && e.Voiture == chauffeur.Voiture && e.DateCourse.Date == date.Date);
        }
    }
}
