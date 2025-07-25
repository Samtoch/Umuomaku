using Umuomaku.Data;
using Umuomaku.Data.Models;

namespace Umuomaku.Repositories
{
    public interface IHighlightRepo
    {
        Task<AdminUser> GetUserDetails(string email, string hashedPassword);
        Task UpdateAdminUserAsync(AdminUser user);
        Task AddHighlightAsync(Highlight highlight);
        Task<List<Highlight>> GetAllHighlightAsync();
        Task<List<Highlight>> GetTopHighlightsAsync(int count);
        Task<List<Highlight>> GetTopHighlightsAsync();
        Task<Highlight?> GetHighlightByIdAsync(int id);
        Task UpdateHighlightAsync(Highlight highlight);
        Task SoftDeleteHighlightAsync(int id);
    }
}

