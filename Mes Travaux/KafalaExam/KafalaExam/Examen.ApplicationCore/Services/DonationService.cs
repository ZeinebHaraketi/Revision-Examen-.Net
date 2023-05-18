
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
    public class DonationService : Service<Donation>, IDonationService
    {
        public DonationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        //implémentation des méthodes
        public decimal GetTotalDonationsForPastPeriod(DateTime startDate, DateTime endDate)
        {
            // Assuming you have a list of donations
            List<Donation> donations = new List<Donation>();

            // Filter donations based on the specified period
            var filteredDonations = donations.Where(d => d.Date >= startDate && d.Date <= endDate);

            // Calculate the total amount of donations
            decimal totalDonations = filteredDonations.Sum(d => d.Amount);

            return totalDonations;
        }

    }
}

  