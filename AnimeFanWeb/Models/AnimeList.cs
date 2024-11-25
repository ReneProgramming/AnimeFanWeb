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

        public DateOnly? EndDate { get; set }

    }

    public class AnimeListEditViewModel
    { 
        public int Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get;set; }
    }

    public class AnimeListIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Anime Title")]
        public string Title { get; set; }

        [Display(Name ="Show/Movie/OVA")]
        public string Type { get; set; }

        public string Genre { get; set; }

        [Display(Name ="Date Aired")]
        public DateOnly StartDate { get; set; }

        [Display(Name ="Date Ended")]
        public DateOnly? EndDate { get; set; }
    }

}
