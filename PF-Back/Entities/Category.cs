using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Categories")]
    public class Category : EntityBase
    {
        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public IList<Thing> Things { get; set; }
    }
}
