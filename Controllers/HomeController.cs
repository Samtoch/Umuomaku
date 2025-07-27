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

        public async Task<IActionResult> Highlights(int id)
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


        public IActionResult HighlightDetail()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult Contactus()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
