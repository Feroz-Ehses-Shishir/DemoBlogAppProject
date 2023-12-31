﻿using DemoBlogAppProject.Models.DomainModel;
using DemoBlogAppProject.Models.EditModel;
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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var x = await pr.GetAsync(Id);
            var t = await tr.GetAllAsync();

            if(x != null)
            {
                var model = new editPost
                {
                    Id = x.Id,
                    Heading = x.Heading,
                    PageTitle = x.PageTitle,
                    Content = x.Content,
                    Author = x.Author,
                    FeaturedImageUrl = x.FeaturedImageUrl,
                    UrlHandle = x.UrlHandle,
                    ShortDescription = x.ShortDescription,
                    PublishedDate = x.PublishedDate,
                    Visible = x.Visible,
                    Tags = t.Select(xx => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = xx.Name,
                        Value = xx.Id.ToString()
                    }),
                    TagId = x.Tags.Select(xx => xx.Id.ToString()).ToArray()
                };

                return View(model);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(editPost x)
        {
            var post = new Post
            {
                Id = x.Id,
                Heading = x.Heading,
                PageTitle = x.PageTitle,
                Content = x.Content,
                Author = x.Author,
                FeaturedImageUrl = x.FeaturedImageUrl,
                UrlHandle = x.UrlHandle,
                ShortDescription = x.ShortDescription,
                PublishedDate = x.PublishedDate,
                Visible = x.Visible,
            };

            var tg = new List<Tag>();
            foreach (var t in x.TagId)
            {
                var temp = Guid.Parse(t);
                var ts = await tr.GetAsync(temp);

                if (ts != null)
                {
                    tg.Add(ts);
                }

            }
            post.Tags = tg;

            // Submit to repo
            var f = await pr.UpdateAsync(post);

            if(f!=null) 
            {
                //success
                return RedirectToAction("Show");
            }

            //error
            return RedirectToAction("Show");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(editPost x)
        {
            var d = await pr.DeleteAsync(x.Id);

            if (d != null)
            {
                return RedirectToAction("Show");
            }

            return RedirectToAction("Show");
        }
    }
}
