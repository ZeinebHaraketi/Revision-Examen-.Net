
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Examen.ApplicationCore.Domain
    {
        public class Kafala
        {
            //prop+2tab
            [Required]

            public DateTime StartDate { get; set; }

            [Required]

            public DateTime EndDate { get; set; }
       
        
            [DataType(DataType.Currency)]
            public int Amount { get; set; }

            public virtual int BeneficiaryFk { get; set; }
            public virtual int DonatorFk { get; set; }

            [ForeignKey("BeneficiaryFk")]
            public virtual Beneficiary Beneficiary { get; set; }

            [ForeignKey("DonatorFk")]
            public virtual Donator Donator { get; set; }

      
            //prop de navigation: virtual

        }
    }

