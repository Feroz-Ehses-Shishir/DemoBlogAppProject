using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoBlogAppProject.Models.ViewModel
{
    public class AddPost
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        // to display tags httpget
        public IEnumerable<SelectListItem> Tags { get; set; }

        //capture the tags
        public string[] TagId { get; set; } = Array.Empty<string>();
    }
}
