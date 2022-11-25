using Entities;

namespace WebApplicationMVC.Services
{
    public interface IThingService
    {
        List<Thing> GetAll(string? description);

        Thing GetById(int id);

        void Save(Thing thing);
        
        void Update(Thing thing);

        void Delete(Thing thing);
    }
}
