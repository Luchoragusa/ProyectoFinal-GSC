using Entities;

namespace WebApplicationAPI.Handlres
{
    public interface IJwtHandler
    {
        string GenerateToken(Person person);
    }
}
