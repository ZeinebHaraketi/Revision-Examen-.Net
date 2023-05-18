
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Beneficiary
    {
        //prop+2tab
        [Key]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CIN must be an 8-digit number.")]
        public int CIN { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone number must be 8 digits.")]
        public virtual Contact Contact { get; set; }
        public string Description { get; set; }



        //prop de navigation: virtual

    }
}

