using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public class ThingsViewModel
    {
        public int Id { get; set; }
        
       // [Required(ErrorMessage = "Descrpicion es obligatoria")]
       // [MaxLength(10, ErrorMessage = "La descripcion solo puede tener 10 caracteres.")]
        public string Description { get; set; }

       // [Required(ErrorMessage = "La id de categoria es obligatoria.")]
        public int CategoryId { get; set; }
    }
}
