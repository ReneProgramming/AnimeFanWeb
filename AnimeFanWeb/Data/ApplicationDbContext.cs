using AnimeFanWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimeFanWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Moderator> Moderators { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<AnimeList> AnimeList { get; set; }

        public DbSet<UserAnime> UserAnime { get; set; }


    }
}
