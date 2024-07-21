using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using static FurnitureApp.Pages.Shared._HeaderModel;

namespace FurnitureApp.Pages;

public class LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger,
    UserManager<User> userManager, IUserRepository userRepository,
    IDistributedCache cache) : PageModel
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly ILogger<LoginModel> _logger = logger;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IDistributedCache _cache = cache;
    public string? ProviderDisplayName { get; set; }
    public string? ReturnUrl { get; set; }

    [TempData]
    public string? ErrorMessage { get; set; }
    public void OnGet()
    {
    }

    public IActionResult? OnPost(string? provider, string? returnUrl = null)
    {
        if (string.IsNullOrEmpty(provider))
        {
            return NotFound("Service not correct" + provider);
        }

        if (provider == "Google" || provider == "Facebook")
        {
            var redirectUrl = Url.Page("./Login", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        return null;
    }

    public async Task<IActionResult> OnGetCallbackAsync(string? returnUrl = null, string? remoteError = null)
    {
        returnUrl = returnUrl ?? Url.Content("~/");
        if (remoteError != null)
        {
            ErrorMessage = $"Provider Error: {remoteError}";
            _logger.LogError($"Provider Error: {remoteError}");
            return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
        }

        // Take information from external services
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            ErrorMessage = "Error From Login Service.";
            _logger.LogError("Error From Login Service.");
            return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
        }
        // Take user information
        var user = info.Principal;

        var emailUser = user.FindFirst(ClaimTypes.Email)?.Value;
        var nameUser = user.FindFirst(ClaimTypes.Name)?.Value;
        string idUser = "";

        if (emailUser != null)
        {
            User u = _userRepository.GetByEmail(emailUser);
            if (u != null)
            {
                idUser = u.Id;
            }
        }
        // Create user in database
        if (emailUser == null || nameUser == null)
        {
            return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
        }

        var foundUserAsync = await _userManager.FindByEmailAsync(emailUser);
        if (foundUserAsync == null)
        {
            User newUser = new()
            {
                UserName = nameUser,
                Email = emailUser,
            };
            var createUser = await _userManager.CreateAsync(newUser);
            if (createUser == null)
            {
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            User? foundUserByEmail = _userRepository.GetByEmail(newUser.Email);
            if (foundUserByEmail == null)
            {
                _userRepository.Create(newUser);
            }
        }

        // Create Session
        var sessionId = Guid.NewGuid().ToString();
        var headerModel = new HeaderModel
        {
            UserName = nameUser,
            UserEmail = emailUser,
            IsUserLogined = true,
        };
        var jsonHeaderModel = JsonConvert.SerializeObject(headerModel);

        var sessionData = Encoding.UTF8.GetBytes(jsonHeaderModel);
        var sessionOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        };
        await _cache.SetAsync(sessionId, sessionData, sessionOptions);

        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1),
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };
        Response.Cookies.Append("sessionId", sessionId, cookieOptions);
        Response.Cookies.Append("userId", idUser, cookieOptions);

        return RedirectToPage("Index", "Home");
    }
}