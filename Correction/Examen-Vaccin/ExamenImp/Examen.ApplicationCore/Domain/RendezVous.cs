using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class RendezVous
    {
        public DateTime DateVaccination { get; set; }
        public int NbrDoses { get; set; }
        public string CodeInfirmiere { get; set; }
        public string CitoyenId { get; set; }
        public int VaccinId { get; set; }
        [ForeignKey("CitoyenId")]
        public virtual Citoyen citoyen { get; set; }
        public virtual Vaccin vaccin { get; set; }
    }
}
