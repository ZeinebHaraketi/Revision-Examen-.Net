using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Admission
    {
        [DataType(DataType.Date)]
        public DateTime DateAdmission { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = " Le motif de l'admission")]
       // [DisplayName("Le motif de l'admission")]
        public string ModifAdmission { get; set; }
        public int NbJours { get; set; }

        public int ChambreFk { get; set; }
        public string PatientFk { get; set; }
        [ForeignKey("ChambreFk")]
        public virtual Chambre Chambre { get; set; }

        [ForeignKey("PatientFk")]
        public virtual Patient Patient { get; set; }

    }
}
