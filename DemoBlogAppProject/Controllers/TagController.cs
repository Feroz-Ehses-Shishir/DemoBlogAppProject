using DemoBlogAppProject.Models.DomainModel;
using DemoBlogAppProject.Models.EditModel;
using DemoBlogAppProject.Models.ViewModel;
using DemoBlogAppProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlogAppProject.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository tagRep;

        public TagController(ITagRepository tagRep)
        {
            this.tagRep = tagRep;
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

            await tagRep.AddAsync(tag);

            return RedirectToAction("Show");
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            var tag = await tagRep.GetAllAsync();

            return View(tag);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var x = await tagRep.GetAsync(Id);

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
            var tag = new Tag
            {
                Id = x.Id,
                Name = x.Name,
                DisplayName = x.DisplayName
            };

            var newTag = await tagRep.UpdateAsync(tag);

            if (newTag!=null)
            {
                return RedirectToAction("Show");
            }

            return RedirectToAction("Edit",new{Id=x.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(editTag x)
        {
            var tag = await tagRep.DeleteAsync(x.Id);

            if (tag != null)
            {
                return RedirectToAction("Show");
            }

            return RedirectToAction("Edit", new { Id = x.Id });
        }
    }
}
