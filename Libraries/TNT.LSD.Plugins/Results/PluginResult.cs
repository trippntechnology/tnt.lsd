
namespace TNT.LSD.Plugins
{
	public class PluginResult
	{
		public bool Success { get; protected set; }

		public PluginResult(bool success)
		{
			Success = success;
		}
	}
}
