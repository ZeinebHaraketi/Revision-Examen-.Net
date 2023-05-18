using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public enum TypeChambre
    {
        simple,Double
    }
    public class Chambre
    {
        public double Prix { get; set; }
        [Key]
        public int NumeroChambre { get; set; }
        public TypeChambre TypeChambre { get; set; }
        public int CliniqueFk { get; set; }
        public virtual Clinique Clinique { get; set; }
        public virtual IList<Admission> Admissions { get; set; }
    }
}
