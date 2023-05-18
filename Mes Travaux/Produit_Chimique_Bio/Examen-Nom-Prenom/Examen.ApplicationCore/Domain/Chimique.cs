using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Chimique: Produit
    {
        public string NomLab { get; set; }
        public string Ville { get; set; }

    }
}
