using DemoBlogAppProject.Models.DomainModel;
using DemoBlogAppProject.Models.ViewModel;
using DemoBlogAppProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlogAppProject.Controllers
{
    public class PostController : Controller
    {
        private readonly ITagRepository tr;
        private readonly IPostRepository pr;

        public PostController(ITagRepository tr, IPostRepository pr)
        {
            this.tr = tr;
            this.pr = pr;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tr.GetAllAsync();

            var model = new AddPost
            {
                Tags = tags.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AddPost x)
        {
            // Map view model to domain model
            var post = new Post
            {
                Heading = x.Heading,
                PageTitle = x.PageTitle,
                Content = x.Content,
                ShortDescription = x.ShortDescription,
                FeaturedImageUrl = x.FeaturedImageUrl,
                UrlHandle = x.UrlHandle,
                PublishedDate = x.PublishedDate,
                Author = x.Author,
                Visible = x.Visible,
            };

            // Map Tags from selected tags
            var tg = new List<Tag>();
            foreach (var t in x.TagId)
            {
                var temp = Guid.Parse(t);
                var ts = await tr.GetAsync(temp);

                if(ts != null)
                {
                    tg.Add(ts);
                }

            }
            post.Tags = tg;

            await pr.AddAsync(post);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            var x = await pr.GetAllAsync();

            return View(x);
        }
    }
}
