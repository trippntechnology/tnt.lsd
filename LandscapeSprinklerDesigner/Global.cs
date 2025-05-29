using LandscapeSprinklerDesigner.Utils;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using TNT.Commons;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner;

public static class Global
{
  public static ApplicationRegistry? userRegistry = null; // Initialized in Main()

  public static void CheckForUpdate(IWin32Window owner, bool showOnlyIfExists = true)
  {
    try
    {
      var registrationKey = FileUtil.GetRegistrationKey();
      if (registrationKey == null)
      {
        //MessageBox.Show(owner, "License key could not be found.", "Missing License Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      string exePath = Assembly.GetExecutingAssembly().Location;
      var exeFolder = Path.GetDirectoryName(exePath);

      if (exeFolder == null) return;

      var sb = new StringBuilder();
      sb.Append($" /i {registrationKey.ApplicationID}");
      sb.Append($" /a \"{exePath}\"");
      sb.Append($" /p {registrationKey.Secret}");
      sb.Append($" /e {registrationKey.ServiceEndpoint}");

      if (showOnlyIfExists) sb.Append($" /s");

      var process = new Process();

      process.StartInfo.FileName = Path.Combine(exeFolder, "updater\\tnt.updater.exe");
      process.StartInfo.Arguments = sb.ToString();

      Debug.WriteLine("Checking for update ...");
      Debug.WriteLine($"\tArgs: {process.StartInfo.Arguments}");
      process.Start();
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      MessageBox.Show(owner, "The update server is unavailable. Please verify you're connected to the internet and try again.", "Update Server Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
  }

  public static string getVersion()
  {
    return Assembly.GetExecutingAssembly().GetName().Version?.Let(version =>
    {
      var values = version.ToString().Split('.').Take(3);
      return String.Join(".", values);
    }) ?? "0.0.0";
  }
}