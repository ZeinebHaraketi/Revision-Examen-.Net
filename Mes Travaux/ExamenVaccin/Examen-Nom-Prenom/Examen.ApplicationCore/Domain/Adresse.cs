using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    [Owned]
    public class Adresse
    {
        public int CodePostal { get; set; }
        public int Rue { get; set; }
        public string Ville { get; set; }



    }
}
