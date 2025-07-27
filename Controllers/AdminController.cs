using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Fluent;
using System.Threading.Tasks;
using Umuomaku.Data.Models;
using Umuomaku.Helpers;
using Umuomaku.Repositories;

namespace Umuomaku.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepo _repo;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public AdminController(IAdminRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                TempData["LoginError"] = "Email and password are required.";
                return View();
            }

            string hashedPassword = PasswordHasher.ComputeStringToSha256Hash(login.Password);

            var user = await _repo.GetUserDetails(login.Email, hashedPassword);

            if (user == null)
            {
                TempData["LoginError"] = "Invalid login credentials.";
                return View();
            }

            // (Optional) Update LastLogin timestamp
            user.LastLogin = DateTime.Now;
            await _repo.UpdateAdminUserAsync(user);

            // Redirect to dashboard or admin home
            return RedirectToAction("Home", "Admin");
        }

        public IActionResult Home()
        {
            return View();
        }


        public async Task<IActionResult> Highlights()
        {
            var highlights = await _repo.GetAllHighlightAsync();
            return View(highlights);
        }

        public async Task<IActionResult> Events()
        {
            var ev = await _repo.GetAllEventsAsync();
            return View(ev);
        }

        [HttpGet]
        public IActionResult CreateHighlights() => View();

        [HttpPost]
        public async Task<IActionResult> CreateHighlights(Highlight model, IFormFile ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string refernce = GetImageReference("HIGHLIGHT");
                    string fileName = refernce + ".png";

                    //var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    model.ImageUrl = fileName;
                }

                model.DateCreated = DateTime.Now;
                model.UserCreated = User.Identity?.Name ?? "System";
                await _repo.AddHighlightAsync(model);

                TempData["Success"] = "Highlight created successfully.";
                return RedirectToAction("Highlights");
            }
            catch (Exception ex)
            {
                log.Error("Error with CreateHighlights. " + ex);
            }
            return RedirectToAction("CreateHighlights");
        }

        [HttpGet]
        public IActionResult CreateEvent() => View();

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event model, IFormFile ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string refernce = GetImageReference("EVENT");
                    string fileName = refernce + ".png";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    model.ImageUrl = fileName;
                }

                model.DateCreated = DateTime.Now;
                model.UserCreated = User.Identity?.Name ?? "System";
                await _repo.AddEventAsync(model);

                TempData["Success"] = "Event created successfully.";
                return RedirectToAction("CreateEvent");
            }
            catch (Exception ex)
            {
                log.Error("Error with CreateEvents. " + ex);
            }
            return RedirectToAction("CreateEvents");
        }

        public string GetImageReference(string idtype)
        {
            string imageId = "";
            Random random = new Random();
            int ranNum = random.Next(10, 99);
            var today = DateTime.Now.ToString("yyMMddHHmmss");
            try
            {
                imageId = "IMG-" + idtype + "-" + today + ranNum.ToString();
            }
            catch (Exception ex)
            {
                log.Error("Error with GetImageReference" + ex);
            }

            return imageId;
        }

        public async Task<IActionResult> UpdateHighlights(int id)
        {
            var highlight = await _repo.GetHighlightByIdAsync(id);
            if (highlight == null)
            {
                return NotFound();
            }
            return View(highlight);
        }

        public async Task<IActionResult> UpdateEvent(int id)
        {
            var ev = await _repo.GetEventByIdAsync(id);
            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHighlights(Highlight model, IFormFile ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string refernce = GetImageReference("HIGHLIGHT");
                    string fileName = refernce + ".png";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    model.ImageUrl = fileName;
                }

                model.DateUpdated = DateTime.Now;
                model.UserUpdated = User.Identity?.Name ?? "System";

                await _repo.UpdateHighlightAsync(model);

                TempData["Success"] = "Highlight updated successfully.";
                return RedirectToAction("UpdateHighlights");
            }
            catch (Exception ex)
            {
                log.Error("Error with UpdateHighlights. " + ex);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEvent(Event model, IFormFile ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string refernce = GetImageReference("EVENT");
                    string fileName = refernce + ".png";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    model.ImageUrl = fileName;
                }

                model.DateUpdated = DateTime.Now;
                model.UserUpdated = User.Identity?.Name ?? "System";

                await _repo.UpdateEventAsync(model);

                TempData["Success"] = "Event updated successfully.";
                return RedirectToAction("UpdateEvent");
            }
            catch (Exception ex)
            {
                log.Error("Error with UpdateEvent. " + ex);
            }
            return View(model);
        }


        public async Task<IActionResult> DeleteHighlight(int id)
        {
            var highlight = await _repo.GetHighlightByIdAsync(id);
            if (highlight == null)
            {
                return NotFound();
            }
            return View(highlight);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHighlightById(int id)
        {
            await _repo.SoftDeleteHighlightAsync(id);
            TempData["Success"] = "Highlight deleted successfully.";
            return RedirectToAction("Highlights");
        }


        public async Task<IActionResult> DeleteEvent(int id)
        {
            var ev = await _repo.GetEventByIdAsync(id);
            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEventById(int id)
        {
            try
            {
                await _repo.SoftDeleteEventAsync(id);
                TempData["DeleteEventResponse"] = "Event deleted successfully";
                return RedirectToAction("Events");
            }
            catch (Exception ex)
            {
                TempData["DeleteEventResponse"] = "Something went wrong. Delete Failed";
                log.Error("Error with DeleteEventById. " + ex);
            }

            return RedirectToAction("Events");
        }
    }
}
