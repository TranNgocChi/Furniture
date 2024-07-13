using static FurnitureApp.Pages.Shared._HeaderModel;

namespace FurnitureApp.Helpers
{
	public interface ISessionHelper
	{
		Task<HeaderModel?> GetSessionAsync(HttpRequest request);
	}
}
