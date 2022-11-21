using Entities;

namespace WebApplicationAPI.Dto
{
    public class PersonDTO : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Role { get; set; }
    }
}
