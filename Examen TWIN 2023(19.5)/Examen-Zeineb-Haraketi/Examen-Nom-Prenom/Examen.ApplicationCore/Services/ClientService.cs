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
    public class ClientService : Service<Client>, IClientService
    {
        public ClientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<RDV> GetRDVs(DateTime dateR)
        {
            List<RDV> rdvs = new List<RDV>();
            //RDV rd = new RDV();
            var q = from r in rdvs
                    select r.Confirmation == true && r.DateRDV== dateR;
            q.ToList();
            return (IEnumerable<RDV>)q;

            //return GetMany(e => e.RDVs. == statut).
            //    Select(e => e.Livre.Categorie).GroupBy(e => e.Code).Select(e => e.First());
        }
    }
}
