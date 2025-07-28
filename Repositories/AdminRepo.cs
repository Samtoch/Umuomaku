using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NLog;
using Umuomaku.Data;
using Umuomaku.Data.Models;

namespace Umuomaku.Repositories
{
    public class AdminRepo : IAdminRepo
    {
        private readonly UmuomakuDbContext _context;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public AdminRepo(UmuomakuDbContext context)
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
        public async Task AddEventAsync(Event ev)
        {
            try
            {
                _context.Events.Add(ev);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("Error with AddEventAsync. " + ex);
            }
        }

        public async Task AddGalleryAsync(Gallery gallery)
        {
            try
            {
                _context.Galleries.Add(gallery);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("Error with AddGalleryAsync. " + ex);
            }
        }

        public async Task<List<Highlight>> GetAllHighlightAsync()
        {
            return await _context.Highlights.Where(h => !h.IsDeleted).ToListAsync();
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events.Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<List<Gallery>> GetAllGalleryAsync()
        {
            return await _context.Galleries.Where(g => !g.IsDeleted).ToListAsync();
        }

        public async Task<List<Highlight>> GetTopHighlightsAsync()
        {
            return await _context.Highlights.OrderByDescending(h => h.DateCreated).ToListAsync();
        }

        public async Task<List<Event>> GetTopEventsAsync()
        {
            return await _context.Events.OrderByDescending(e => e.DateOfEvent).ToListAsync();
        }

        public async Task<List<Highlight>> GetTopHighlightsAsync(int count)
        {
            return await _context.Highlights.Where(h => h.IsDeleted == false)
                .OrderByDescending(h => h.DateCreated)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Event>> GetTopEventsAsync(int count)
        {
            return await _context.Events.Where(e => e.IsDeleted == false)
                .OrderByDescending(e => e.DateCreated)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Gallery>> GetTopGalleryAsync()
        {
            return await _context.Galleries.Where(g => g.IsDeleted == false)
                .OrderByDescending(e => e.DateCreated)
                .ToListAsync();
        }

        public async Task<Gallery?> GetGalleryByIdAsync(int id)
        {
            return await _context.Galleries.FindAsync(id);
        }
        public async Task<Highlight?> GetHighlightByIdAsync(int id)
        {
            return await _context.Highlights.FindAsync(id);
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
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

        public async Task UpdateEventAsync(Event ev)
        {
            try
            {
                _context.Events.Update(ev);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("Error with UpdateEventAsync. " + ex);
            }
        }

        public async Task UpdateGalleryAsync(Gallery gallery)
        {
            try
            {
                _context.Galleries.Update(gallery);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                log.Error("Error with UpdateGalleryAsync. " + ex);
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

        public async Task SoftDeleteEventAsync(int id)
        {
            try
            {
                var ev = await GetEventByIdAsync(id);
                if (ev != null)
                {
                    ev.IsDeleted = true;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error with SoftDeleteEventAsync. " + ex);
            }
        }

        public async Task SoftDeleteGalleryAsync(int id)
        {
            try
            {
                var gallery = await GetGalleryByIdAsync(id);
                if (gallery != null)
                {
                    gallery.IsDeleted = true;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error with SoftDeleteGalleryAsync. " + ex);
            }
        }


    }
}
