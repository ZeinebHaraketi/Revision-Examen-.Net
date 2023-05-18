using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Patient
    {
        [DataType(DataType.EmailAddress)]
        public string AdresseEmail { get; set; }

        public string CIN { get; set; }

        public NomComplet NomComplet { get; set; }
        public DateTime DateNaissance { get; set; }

        [Key]
        public int NumDossier { get; set; }
        public int NumTel { get; set; }


        public virtual IList<Admission> Admissions { get; set; }


    }
}
