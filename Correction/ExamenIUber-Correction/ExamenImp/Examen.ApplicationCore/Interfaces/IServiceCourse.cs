using AM.ApplicationCore.Interfaces;
using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IServiceCourse:IService<Course>
    {
        IEnumerable<Course> GetPayedCourses(Chauffeur chauffeur, DateTime date);
        double GetBenefice(Chauffeur chauffeur, DateTime date);
    }
}
