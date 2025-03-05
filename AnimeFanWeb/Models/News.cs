using System.ComponentModel.DataAnnotations;

namespace AnimeFanWeb.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string RelatedLinks { get; set; }
        public DateTime DatePublished { get; set; }
        public string ImageFileName { get; set; }
    }

    public class NewsCreateViewModel
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public string RelatedLinks { get; set; } 

        public DateTime DatePublished { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class NewsEditViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public string RelatedLinks { get; set; } 

        public DateTime DatePublished { get; set; }
        public string ImageFileName { get; set; }
        public IFormFile ImageFile { get; set;}
    }

    public class NewsIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string RelatedLinks { get; set; } 
        public DateTime DatePublished { get; set; }
        public string ImageFileName { get; set; }
    }


}
