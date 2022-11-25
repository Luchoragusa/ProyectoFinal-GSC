using Entities;
using Microsoft.EntityFrameworkCore;
using WebApplicationMVC.DataAccess;

namespace WebApplicationMVC.Services
{
    public class ThingService : IThingService
    {
        private readonly MVC_Context mVC_Context;

        public ThingService(MVC_Context mVC_Context)
        {
            this.mVC_Context = mVC_Context;
        }

        public List<Thing> GetAll(string? description)
        {
            if (description == null)
                return mVC_Context.Things.ToList();
            else
                return mVC_Context.Things.Where(x => x.Description.Contains(description)).ToList();
        }

        public Thing GetById(int id)
        {
            return mVC_Context.Things.Find(id);
        }

        public void Save(Thing thing)
        {
            mVC_Context.Things.Add(thing);
            mVC_Context.SaveChanges();
        }

        public void Update(Thing thing)
        {
            mVC_Context.Things.Update(thing);
            mVC_Context.SaveChanges();
        }

        public void Delete(Thing thing)
        {
            mVC_Context.Things.Remove(thing);
            mVC_Context.SaveChanges();
        }
    }
}
