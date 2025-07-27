using Umuomaku.Data;
using Umuomaku.Data.Models;

namespace Umuomaku.Repositories
{
    public interface IAdminRepo
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

        Task AddEventAsync(Event ev);
        Task<List<Event>> GetAllEventsAsync();
        Task<List<Event>> GetTopEventsAsync(int count);
        Task<List<Event>> GetTopEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task UpdateEventAsync(Event ev);
        Task SoftDeleteEventAsync(int id);
    }
}

