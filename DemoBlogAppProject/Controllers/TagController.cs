using DemoBlogAppProject.DB;
using DemoBlogAppProject.Models.DomainModel;
using DemoBlogAppProject.Models.EditModel;
using DemoBlogAppProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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

            return RedirectToAction("Show");
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            var tag = db.Tags.ToList();

            return View(tag);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var x = await db.Tags.FindAsync(Id);

            var tag = new editTag
            {
                Id = x.Id,
                Name = x.Name,
                DisplayName = x.DisplayName
            };

            return View(tag);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(editTag x)
        {
            
            var newTag = await db.Tags.FindAsync(x.Id);

            if(newTag!=null)
            {
                newTag.Name = x.Name;
                newTag.DisplayName = x.DisplayName;

                await db.SaveChangesAsync();

                return RedirectToAction("Show");
            }

            return RedirectToAction("Edit",new{Id=x.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(editTag x)
        {
            var tag = await db.Tags.FindAsync(x.Id);

            if (tag != null)
            {
                db.Tags.Remove(tag);
                await db.SaveChangesAsync();

                return RedirectToAction("Show");
            }

            return RedirectToAction("Edit", new { Id = x.Id });
        }
    }
}
