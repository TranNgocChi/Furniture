using Microsoft.AspNetCore.Mvc;
namespace FurnitureApp.Pages.Shared;

public class _HeaderModel : ViewComponent
{
    public class HeaderModel
    {
        public string? UserEmail { get; set; }
        public string? UserName { get; set; }
        public bool? IsUserLogined { get; set; }
        public string CurrentTab { get; set; } = string.Empty;
    }
}
