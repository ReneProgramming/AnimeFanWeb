using AnimeFanWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AnimeFanWeb.Models
{
    public interface IModeratorRepository
    {
        Task SaveModerator(Moderator moderator);
        Task<IEnumerable<Moderator>> GetAllModerators();
        Task DeleteModerator(int moderator);
        Task UpdateModerator(Moderator moderator);
        Task<Moderator?> GetModerator(int moderatorId);
    }

    public class ModeratorRepository : IModeratorRepository
    {
        private ApplicationDbContext _context;

        public ModeratorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteModerator(int moderatorId)
        {
            Moderator? moderator = await GetModerator(moderatorId);

            if (moderator != null)
            {
                // Change all related events to a null moderator
                using DbConnection con = _context.Database.GetDbConnection();
                await con.OpenAsync();
                using DbCommand query = con.CreateCommand();
                query.CommandText = "UPDATE Events SET ModeratorId = null WHERE ModeratorId = " + moderator.Id;
                int rowsAffected = await query.ExecuteNonQueryAsync();

                // Remove moderator from database
                _context.Moderators.Remove(moderator);
                await _context.SaveChangesAsync();                
            }
        }

        public async Task<IEnumerable<Moderator>> GetAllModerators()
        {
            return await _context.Moderators.OrderBy(moderator => moderator.FullName).ToListAsync();
        }

        public async Task <Moderator?> GetModerator(int moderatorId)
        {
            return await _context.Moderators.SingleOrDefaultAsync(m => m.Id == moderatorId);
        }

        public async Task SaveModerator(Moderator moderator)
        {
            _context.Moderators.Add(moderator);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateModerator(Moderator moderator)
        {
            _context.Add(moderator);
            await _context.SaveChangesAsync();
        }
    }
}
