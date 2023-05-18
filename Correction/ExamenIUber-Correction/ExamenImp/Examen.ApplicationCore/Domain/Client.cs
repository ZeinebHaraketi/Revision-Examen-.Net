using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Client
    {
        [Key]
        public string CIN { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [Required]
        [EmailAddress]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateNaissance { get; set; }
        public virtual IList<Course> Courses { get; set; }
    }
}
