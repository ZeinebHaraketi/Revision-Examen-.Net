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
    public class CourseService : Service<Course>, ICourseService
    {
        public CourseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Course> getCoursesPayee(Chauffeur c, DateTime date)
        {
            return GetMany(t => t.Etat == Etat.Payee && t.Voiture == c.Voiture && t.DateCourse == date);
        }
        public double BeneficeTotal( Chauffeur c,DateTime date)
        {
            return getCoursesPayee(c, date).Sum(t => t.Montant * c.TauxBenefice);
        }
    }
}
