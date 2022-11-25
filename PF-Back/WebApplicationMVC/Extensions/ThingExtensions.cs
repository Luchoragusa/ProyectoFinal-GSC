using Entities;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Extensions
{
    public static class ThingExtensions
    {
        public static Thing ToEntity(this ThingsViewModel thing)
        {
            return new Thing
            {
                Id = thing.Id,
                Description = thing.Description,
                CategoryId = thing.CategoryId
            };
        }

        public static List<ThingsViewModel> ToViewModels(this List<Thing> things)
        {
            var list = new List<ThingsViewModel>();
            things.ForEach(x => list.Add(x.ToViewModel()));
            return list;
        }
        
        public static ThingsViewModel ToViewModel(this Thing thing)
        {
            return new ThingsViewModel
            {
                Id = thing.Id,
                Description = thing.Description,
                CategoryId = thing.CategoryId
            };
        }
    }
}
