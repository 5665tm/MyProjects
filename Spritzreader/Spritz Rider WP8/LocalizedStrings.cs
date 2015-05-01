using Spritz_Rider_WP8.Resources;

namespace Spritz_Rider_WP8
{
	/// <summary>
	/// Предоставляет доступ к строковым ресурсам.
	/// </summary>
	public class LocalizedStrings
	{
		private static AppResources _localizedResources = new AppResources();

		public AppResources LocalizedResources { get { return _localizedResources; } }
	}
}