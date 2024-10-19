using System.ComponentModel.DataAnnotations;

namespace AnimeFanWeb.Models
{
    public class Moderator
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
    }
}
