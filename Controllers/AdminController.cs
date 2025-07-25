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
        private readonly IHighlightRepo _repo;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public AdminController(IHighlightRepo repo)
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

        [HttpGet]
        public IActionResult CreateHighlights() => View();

        [HttpPost]
        public async Task<IActionResult> CreateHighlights(Highlight model, IFormFile ImageFile)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string refernce = GetImageReference();
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
        public string GetImageReference()
        {
            string imageId = "";
            Random random = new Random();
            int ranNum = random.Next(10, 99);
            var today = DateTime.Now.ToString("yyMMddHHmmss");
            try
            {
                imageId = "IMG" + today + ranNum.ToString();
            }
            catch (Exception ex)
            {
                //log.Error("Error with GetImageReference" + ex);
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

        [HttpPost]
        public async Task<IActionResult> UpdateHighlights(Highlight model, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                model.ImageUrl = fileName;
            }

            model.DateUpdated = DateTime.Now;
            model.UserUpdated = User.Identity?.Name ?? "System";

            _repo.UpdateHighlightAsync(model);

            TempData["Success"] = "Highlight updated successfully.";
            return RedirectToAction("Highlights");
        }


        public IActionResult DeleteHighlights(int id)
        {
            _repo.SoftDeleteHighlightAsync(id);
            TempData["Success"] = "Highlight deleted successfully.";
            return RedirectToAction("Highlights");
        }
    }
}
