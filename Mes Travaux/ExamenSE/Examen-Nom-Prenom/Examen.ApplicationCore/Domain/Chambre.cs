using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Chambre
    {
        [Key]
        public int NumeroChambre { get; set; }
        public float Prix { get; set; }

        public TypeChambre TypeChambre { get; set; }

        public int CliniqueFK { get; set; }
        public virtual Clinique Clinique { get; set; }

        public virtual IList<Admission> Admissions { get; set; }


    }
}
