
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Donator
    {
        //prop+2tab
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        public string Profession { get; set; }

        public Contact Contact { get; set; }



        public virtual IList<Donation> Donations { get; set; }








        //prop de navigation: virtual

    }
}

