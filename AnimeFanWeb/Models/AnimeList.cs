using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace AnimeFanWeb.Models
{
    public class AnimeList
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }

    public class AnimeListCreateViewModel
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }

    public class AnimeListEditViewModel
    { 
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string Type { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get;set; }
    }

    public class AnimeListIndexViewModel
    {
        public int AnimeListId { get; set; }

        [Display(Name = "Anime Title")]
        public string AnimeTitle { get; set; }

        [Display(Name ="TV/Movie/OVA")]
        public string AnimeType { get; set; }

        [Display(Name = "Genre")]
        public string AnimeGenre { get; set; }

        [Display(Name ="Date Aired")]
        public DateOnly AnimeStartDate { get; set; }

        [Display(Name ="Date Ended")]
        public DateOnly? AnimeEndDate { get; set; }
    }

}
