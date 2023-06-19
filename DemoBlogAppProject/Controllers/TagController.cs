using DemoBlogAppProject.DB;
using DemoBlogAppProject.Models.DomainModel;
using DemoBlogAppProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlogAppProject.Controllers
{
    public class TagController : Controller
    {
        private readonly Appdbcontext db;

        public TagController(Appdbcontext  db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTag x)
        {
            var tag = new Tag
            {
                Name = x.Name,
                DisplayName = x.DisplayName
            };

            await db.Tags.AddAsync(tag);
            await db.SaveChangesAsync();

            return View("Add");
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {


            return View("Add");
        }

    }
}
