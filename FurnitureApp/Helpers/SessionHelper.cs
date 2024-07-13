using DataAccess.Repository.IRepository;
using FurnitureApp.Models;
using FurnitureApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using static FurnitureApp.Pages.Shared._HeaderModel;

namespace FurnitureApp.Helpers;

public class SessionHelper(IDistributedCache cache) : ISessionHelper
{
	private readonly IDistributedCache _cache = cache;

	public async Task<HeaderModel?> GetSessionAsync(HttpRequest request)
	{
		var sessionId = request.Cookies["sessionId"];
		if (!String.IsNullOrEmpty(sessionId))
		{
			var sessionData = await _cache.GetAsync(sessionId);
			if (sessionData != null)
			{
				string sessionStr = Encoding.UTF8.GetString(sessionData);
				var headerModel = JsonConvert.DeserializeObject<HeaderModel>(sessionStr);
				return headerModel;
			}
		}
		return null;
	}
}
