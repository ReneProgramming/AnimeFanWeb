using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnimeFanWeb.Models
{
    public class UserAnime
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        [Required]
        public int AnimeListId { get; set; }

        [ForeignKey("AnimeListId")]
        public AnimeList Anime { get; set; }

        [Required]
        public WatchStatus Status { get; set; }
    }

    public enum WatchStatus
    {
        PlanToWatch,
        Watching,
        Done
    }
}
