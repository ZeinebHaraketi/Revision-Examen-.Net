using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    //Owned ==> installer le package Entity Framework Core 
    [Owned]
    public class NomComplet
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

    }
}
