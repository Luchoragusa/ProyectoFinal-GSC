using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public abstract class EntityBase
    {
        [Key] // Primary Key attribute
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincrement attribute
        public int Id { get; set; }
    }
}