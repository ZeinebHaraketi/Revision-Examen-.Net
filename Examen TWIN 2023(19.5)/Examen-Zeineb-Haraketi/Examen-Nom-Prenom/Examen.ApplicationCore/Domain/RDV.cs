using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class RDV
    {
        public bool Confirmation { get; set; }
        public DateTime DateRDV { get; set; }

        public int PrestationFK { get; set; }
        public int ClientFK { get; set; }
        public virtual Prestation Prestation { get; set; }
        public virtual Client Client { get; set; }


    }
}
