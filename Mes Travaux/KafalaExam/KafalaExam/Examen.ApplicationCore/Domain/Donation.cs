
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Donation
    {
        //prop+2tab
        [Key]

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Amount { get; set; }

        public int DonatorFk { get; set; }

        public virtual Donator Donator { get; set; }


    }
}

