using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Citoyen
    {
        public int CitoyenId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
        public int Telephone { get; set; }

        [Key]
        public string CIN { get; set; }

        [Required(ErrorMessage = "champs obligatoire")]
        public int NumeroEvax { get; set; }
        public Adresse Adresse { get; set; }

        public virtual IList<RendezVous> rendezVous { get; set; }
    }
}
