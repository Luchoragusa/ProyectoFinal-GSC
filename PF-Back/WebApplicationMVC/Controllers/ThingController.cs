using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;
using WebApplicationMVC.Services;
using WebApplicationMVC.Extensions;

namespace WebApplicationMVC.Controllers
{
    public class ThingController : Controller
    {
        private readonly IThingService thingService;

        public ThingController(IThingService thingService)
        {
            this.thingService = thingService;
        }

        
        // GET: Thing
        public ActionResult Index()
        {
            var things = thingService.GetAll(null);
            var thingsViewModel = things.ToViewModels();
            return View(thingsViewModel);
        }

        // GET: Thing/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var thing = thingService.GetById(id.Value);
            if (thing == null)
                return NotFound();

            return View(thing.ToViewModel());
        }

        // GET: Thing/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Thing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThingsViewModel thingsViewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", thingsViewModel);

            var list = thingService.GetAll(thingsViewModel.Description); //simulemos que validamos duplicados.
            
            if (list.Any(a => a.Description == thingsViewModel.Description))
            {
                ModelState.AddModelError(String.Empty, "Already exists a thing with the same description.");
                return View(thingsViewModel);
            }

            var entity = thingsViewModel.ToEntity();
            entity.CreationDate = DateTime.UtcNow;
            thingService.Save(entity);
            return RedirectToAction(nameof(Index));
        }

        // GET: Thing/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var thing = thingService.GetById(id.Value);
            
            if (thing == null)
                return NotFound();

            return View(thing.ToViewModel());
        }

        // POST: Thing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ThingsViewModel thingsViewModel)
        {
            if (id != thingsViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                thingService.Update(thingsViewModel.ToEntity());
                return RedirectToAction(nameof(Index));
            }
            return View(thingsViewModel);
        }

        // GET: Thing/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            
            var thing = thingService.GetById(id.Value);
            if (thing == null)
                return NotFound();
            
            return View(thing.ToViewModel());
        }

        // POST: Thing/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var thing = thingService.GetById(id);
            if (thing == null)
                return NotFound();
            
            thingService.Delete(thing);
            return RedirectToAction(nameof(Index));
        }
    }
}
