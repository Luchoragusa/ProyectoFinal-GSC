using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Entities
{
    [Table("Loans")]
    public class Loan : EntityBase
    {
        public DateTime? LoanDate { get; set; }
        [AllowNull]
        public DateTime?  ReturnDate { get; set; }
        
        // Cosa
        public int ThingId { get; set; }
        public Thing Thing { get; set; }
        
        // Persona
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
