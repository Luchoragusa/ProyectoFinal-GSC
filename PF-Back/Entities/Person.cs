using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Person")]
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public string User { get; set; }
        //public string Password { get; set; }
        public IList<Loan> Loans { get; set; }
    }
}
