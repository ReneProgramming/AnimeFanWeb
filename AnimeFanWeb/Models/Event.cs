using System.ComponentModel.DataAnnotations;

namespace AnimeFanWeb.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Moderator Moderator { get; set; }
    }

    public class EventCreateViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<Moderator> AllAvailableModerators { get; set; }

        public Moderator ChosenModerator { get; set; }
    }
}
