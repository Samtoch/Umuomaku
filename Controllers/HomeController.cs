using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Umuomaku.Models;

namespace Umuomaku.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Highlights(int id)
        {
            var highlight = GetHighlights().FirstOrDefault(h => h.Id == id);
            if (highlight == null) return NotFound();
            return View("HighlightDetail", highlight);
        }

        private List<Highlight> GetHighlights()
        {
            return new List<Highlight>
        {
            new Highlight
            {
                Id = 1,
                Title = "Our new Igwe-Elect, Chief Livinus Ezenwa gets certified by Governor Chukwuma Soludo",
                ImageUrl = "~/images/gallery/Igwe-with-the-governor.jpg",
                Content = @"On the 24th of September 2022, the people of Umuomaku went to the poll to elect their new Igwe. The election was conducted between Ifeanyi Peter Ogbugha (Nwawelugo) and Chief Livinus Ezenwa (Onwa). 

The UPU exco led by the PG Sir Augustine Chukwuekezie (Egbe eji eje ogu) and the representative from the Commissioner for Local Government & Chieftaincy Affairs – Anambra State supervised the election. After the initial accreditation of eligible voters, the election was conducted peacefully and Chief Livinus Ezenwa was elected the Igwe of Umuomaku after getting the highest votes.

On Thursday 8th of December, Governor Chukwuma Soludo presented the certificate of recognition to the Igwe-Elect of Umuomaku at the Government House Awka. He promised that his administration strategy is to foster peace, unity, and progress in all communities in Anambra state. The governor was also clear that his government will not interfere in the process of selecting traditional rulers in any community in Anambra.
 
HRH Igwe Livinus Ezenwa thank the governor for his leadership in building a viable state and promised to work hard in uniting the people of Umuomaku for effective community development."
            },
            new Highlight
            {
                Id = 2,
                Title = "Proposed Ezira-Umuomaku-Enugu Umuonyia - Achina road construction project begins",
                ImageUrl = "~/images/gallery/RoadConstruction.jpg",
                Content = @"After the Anambra State Executive Council awarded the road construction of Ezira-Umuomaku-Enugu-Umuonyia -Achina, the construction continued from the Ezira axis. The road will boost food production and aid the evacuation of farm produce to various parts of Anambra State and Nigeria.

The road is 12.5 km long and 8m wide and will link 4 communities Ezira, Umuomaku, Enugu Umuonyia, and Achina. The contract was awarded to Tamad Construction limited with a completion period of 12 months."
            },
            new Highlight
            {
                Id = 3,
                Title = "Umuomaku gets new President General",
                ImageUrl = "~/images/gallery/PgAndExcos.jpg",
                Content = @"On Saturday 25th June 2022, the people of Umuomaku conducted the PG election. This was supervised by the representative from Commissioner for Local Government & Chieftaincy Affairs – Anambra State.

The election was between Augustine Chukwuekezie and Chidi Uzor. After the election, Augustine Chukwuekezie was elected as the President General of the town.

The following members was also elected to the executive of Umuomaku Progressive Union (UPU).

The officers are listed below;

Augustine Chukwuekezie – President General

Chukwujekwu Ibe – Vice President General

Chukwudi A. Nnamdi – General secretary

Kanayo Okeke – Vice Secretary

Sunday Unaka – Treasurer

Obi Ezenkwere – Financial Secretary

Andy Offia – Social Secretary

Ekene W. Umeorah – P.R.O

Bernard Uzor – Provost Umunambu

Izuchukwu Nnorom - Provost Okpobe

Tochukwu Ugboaja - Provost Umuokpulukpu

Mathias Igbonefo - Provost Umungada"
            }
        };
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
