
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Examen.ApplicationCore.Domain
{
    [Owned]
    public class Contact
    {
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone number must be 8 digits.")]
        public string Phone { get; set; }
        
        public string Adress { get; set; }

        public string Email { get; set; }
    }
}

