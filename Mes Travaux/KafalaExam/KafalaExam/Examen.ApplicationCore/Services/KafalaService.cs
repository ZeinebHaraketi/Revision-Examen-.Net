
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
    public class KafalaService : Service<Kafala>, IKafalaService
    {
        public KafalaService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        //implémentation des méthodes
        public List<Beneficiary> GetBeneficiariesWithKafala()
        {
            {
                // Assuming you have a list of beneficiaries and kafalas
                List<Beneficiary> beneficiaries = new List<Beneficiary>();
                List<Kafala> kafalas = new List<Kafala>();

                // Get the current moment
                DateTime now = DateTime.Now;

                // Query to filter beneficiaries with kafala based on the current moment
                var result = from beneficiary in beneficiaries
                             join kafala in kafalas on beneficiary.CIN equals kafala.BeneficiaryFk
                             where kafala.StartDate <= now && kafala.EndDate >= now
                             select beneficiary;

                return result.ToList();
            }


        }
    }
}
  