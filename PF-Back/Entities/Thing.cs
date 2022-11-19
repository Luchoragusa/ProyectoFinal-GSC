using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Things")]
    public class Thing : EntityBase
    {
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
        public IList<Loan> Loans { get; set; }
        // Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
}
