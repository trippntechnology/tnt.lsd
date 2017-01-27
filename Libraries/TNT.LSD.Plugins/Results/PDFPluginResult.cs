
namespace TNT.LSD.Plugins
{
	public class PDFPluginResult: PluginResult
	{
		public string FileName { get; set; }

		public PDFPluginResult(bool success, string fileName)
			: base(success)
		{
			FileName = fileName;
		}
	}
}
