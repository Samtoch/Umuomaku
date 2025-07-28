using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;
using System.Threading.Tasks;
using Umuomaku.Data.Models;
using Umuomaku.Repositories;

namespace Umuomaku.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private readonly ILogger<HomeController> _logger;
        private readonly IAdminRepo _repo;
        public HomeController(ILogger<HomeController> logger, IAdminRepo repo)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var topHighlights = await _repo.GetTopHighlightsAsync(3); // Fetch top 3 by date
            return View(topHighlights);
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Highlights()
        {
            var topHighlights = await _repo.GetTopHighlightsAsync();
            return View(topHighlights);
        }

        public async Task<IActionResult> HighlightDetail(int id)
        {
            var highlight = await _repo.GetHighlightByIdAsync(id);
            if (highlight == null) return NotFound();
            return View("HighlightDetail", highlight);
        }

        public async Task<IActionResult> OurHighlight()
        {
            var topHighlights = await _repo.GetTopHighlightsAsync(); 
            return View(topHighlights);
        }

        public async Task<IActionResult> OurEvents()
        {
            var topHighlights = await _repo.GetTopHighlightsAsync(); 
            return View(topHighlights); 
        }

        public async Task<IActionResult> Events()
        {
            var topHighlights = await _repo.GetTopEventsAsync();
            return View(topHighlights);
        }

        public IActionResult History()
        {
            return View();
        }

        public async Task<IActionResult> Gallery()
        {
            
            var galleryItems = await _repo.GetTopGalleryAsync();
            return View(galleryItems);
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Home/Error")]
        public IActionResult Error()
        {
            try
            {
                var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                if (feature != null)
                {
                    log.Error("Unhandled error at " + feature.Error + " feature.Path: " + feature.Path);
                    //_logger.LogError(feature.Error, $"Unhandled error at {feature.Path}");
                }

                var model = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return View(model);
            }
            catch (Exception ex)
            {
                log.Error("Error with Application. " + ex);
            }

            return View();
        }
    }
}
