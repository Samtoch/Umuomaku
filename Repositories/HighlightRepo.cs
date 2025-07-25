using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NLog;
using Umuomaku.Data;
using Umuomaku.Data.Models;

namespace Umuomaku.Repositories
{
    public class HighlightRepo : IHighlightRepo
    {
        private readonly UmuomakuDbContext _context;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public HighlightRepo(UmuomakuDbContext context)
        {
            _context = context;
        }

        public async Task<AdminUser> GetUserDetails(string email, string hashedPassword)
        {
            var user = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hashedPassword);
            return user;
        }

        public async Task UpdateAdminUserAsync(AdminUser user)
        {
            _context.AdminUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddHighlightAsync(Highlight highlight)
        {
            try
            {
                _context.Highlights.Add(highlight);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("Error with AddHighlightAsync. " + ex);
            }
        }

        public async Task<List<Highlight>> GetAllHighlightAsync()
        {
            return await _context.Highlights.Where(h => !h.IsDeleted).ToListAsync();
        }

        public async Task<List<Highlight>> GetTopHighlightsAsync()
        {
            return await _context.Highlights.OrderByDescending(h => h.DateCreated).ToListAsync();
        }

        public async Task<List<Highlight>> GetTopHighlightsAsync(int count)
        {
            return await _context.Highlights
                .OrderByDescending(h => h.DateCreated)
                .Take(count)
                .ToListAsync();
        }


        public async Task<Highlight?> GetHighlightByIdAsync(int id)
        {
            return await _context.Highlights.FindAsync(id);
        }

        public async Task UpdateHighlightAsync(Highlight highlight)
        {
            try
            {
                _context.Highlights.Update(highlight);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("Error with UpdateHighlightAsync. " + ex);
            }
        }

        public async Task SoftDeleteHighlightAsync(int id)
        {
            try
            {
                var highlight = await GetHighlightByIdAsync(id);
                if (highlight != null)
                {
                    highlight.IsDeleted = true;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error with SoftDeleteHighlightAsync. " + ex);
            }
        }
    }
}
