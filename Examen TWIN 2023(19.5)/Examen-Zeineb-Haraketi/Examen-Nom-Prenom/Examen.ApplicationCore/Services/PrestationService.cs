using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Examen.Interfaces;
using Examen.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class PrestationService : Service<Prestation>, IPrestationService
    {
        public PrestationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public double getPrixMoyen()
        {
            return GetMany(t => t.PrestationType == Domain.Type.Coiffure).Average(t => t.Prix);
        }

        public IEnumerable<Prestataire> threePres(Prestataire p)
        {
            List<Prestataire> lp = new List<Prestataire>();
            var max = NoteMax();


            return (IEnumerable<Prestataire>)GetMany(t => t.Prestataire.Note >= max && t.Prestataire.Zone == Zone.Raoued).OrderByDescending(t => t.Prestataire.PrestataireNom).Take(3);
            throw new NotImplementedException();
        }

        public int NoteMax()
        {
            return GetAll().Max(t=> t.Prestataire.Note);
        }
    }
}
