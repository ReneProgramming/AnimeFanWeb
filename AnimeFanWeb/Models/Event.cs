using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnimeFanWeb.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? ModeratorId { get; set; }

        public Moderator Moderator { get; set; }

        public DateTime EventDate { get; set; }
    }

    public class EventCreateViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<Moderator>? AllAvailableModerators { get; set; }

        public int ChosenModerator { get; set; }

        public DateTime? EventDate { get; set; }
    }

    public class EventEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Event date is required")]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Moderator")]
        public int? ModeratorId { get; set; }

        public List<SelectListItem>? AllModerators { get; set; }
    }


    public class EventIndexViewModel
    { 
        public int EventId { get; set; }

        [Display(Name = "Event Name")]
        public string EventTitle { get; set; }

        [Display(Name = "Moderator")]
        public string ModeratorName { get; set; }

        [Display(Name ="Event Date")]
        public DateTime EventDate { get; set; }
    }
}

