using DataAccess.Repository.IRepository;
using FurnitureApp.Helpers;
using FurnitureApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FurnitureApp.Pages
{
    public class ProfileUserModel(ISessionHelper _sessionHelper,
        IUserRepository _userRepository) : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            string sessionId = Request.Cookies["userId"];

            if (sessionId == "")
            {
                return NotFound();
            }

            ProfileUser = _userRepository.GetById(sessionId);
            if (ProfileUser == null)
            {
                return NotFound();
            }
            ViewData["Header"] = await _sessionHelper.GetSessionAsync(Request);
            return Page();
        }
        [BindProperty]
        public User ProfileUser { get; set; }
        [BindProperty]
        public IFormFile Avatar { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            string sessionId = Request.Cookies["userId"];
            string oldImagePath = "";
            if (sessionId != "")
            {
                User oldUser = _userRepository.GetById(sessionId);
                if (oldUser != null)
                {
                    oldImagePath = oldUser.UserImage;
                    ProfileUser.UserImage = oldUser.UserImage;
                }
            }
            if (oldImagePath != null && Avatar != null)
            {
                var oldFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                var fullPath = Path.Combine(oldFolderPath, Path.GetFileName(oldImagePath));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            if (Avatar != null)
            {
                var fileName = Path.GetFileName(Avatar.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Avatar.CopyToAsync(stream);
                }
                ProfileUser.UserImage = "/uploads/" + fileName;
            }
            if (ProfileUser == null)
            {
                return NotFound();
            }
            _userRepository.Update(ProfileUser);

            return await OnGetAsync();
        }
    }


}
