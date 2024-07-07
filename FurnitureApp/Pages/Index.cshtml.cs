using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using static FurnitureApp.Pages.Shared._HeaderModel;

namespace FurnitureApp.Pages
{
    public class IndexModel(ILogger<IndexModel> logger, IDistributedCache cache) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;
        private readonly IDistributedCache _cache = cache;

        public string? UserName {  get; set; }
        public bool IsUserLogined {  get; set; }


        public async Task OnGet()
        {
            var sessionId = Request.Cookies["sessionId"];
            if (!String.IsNullOrEmpty(sessionId))
            {
                var sessionData = await _cache.GetAsync(sessionId);
                if (sessionData != null)
                {
                    UserName = Encoding.UTF8.GetString(sessionData);
                    IsUserLogined = true;
                }
            }
            else
            {
                IsUserLogined = false;
            }
            var headerModel = new HeaderModel
            {
                UserName = UserName,
                IsUserLogined = IsUserLogined
            };
			ViewData["Header"] = headerModel;
		}
    }
}
