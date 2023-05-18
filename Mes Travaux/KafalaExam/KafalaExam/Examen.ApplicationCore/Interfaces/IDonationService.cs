
using Examen.ApplicationCore.Domain;
using Examen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IDonationService:IService<Donation>
    {
        //signature des méthodes
        public decimal GetTotalDonationsForPastPeriod(DateTime startDate, DateTime endDate);

    }
}

  