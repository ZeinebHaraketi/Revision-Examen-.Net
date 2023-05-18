using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Admission
    {
        [DataType(DataType.Date)]
        public DateTime DateAdmission { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Le motif de l'admission")]
        public string MotifAdmission { get; set; }
        public int NbJours { get; set; }

        public int ChambreFK { get; set; }
        public virtual Chambre Chambre { get; set; }

        public int PatientFk { get; set; }
        public virtual Patient Patient { get; set; }


    }
}
