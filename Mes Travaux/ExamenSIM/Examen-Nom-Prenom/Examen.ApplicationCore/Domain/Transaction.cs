using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Transaction
    {
        public DateTime Date { get; set; }

        public double Montant { get; set; }
        public string CompteFk { get; set; }
        public string DabFK { get; set; }
        public virtual Compte Compte { get; set; }
        public virtual DAB DAB { get; set; }

        /*public int NumeroCompteFk { get; set; }

        [ForeignKey("NumeroCompteFk")]
        public Compte Compte { get; set; }

        public int DABFK { get; set; }

        [ForeignKey("DABFK")]
        public DAB DAB { get; set; }*/


    }
}
