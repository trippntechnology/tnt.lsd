using System.Diagnostics;
using System.Reflection;
using System.Text;
using TNT.Commons;
using TNT.Cryptography;
using TNT.LiveData;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner;

public static class Global
{
  private static string _license_path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "license.txt");
  public static LiveData<License> LicenseLive = new LiveData<License>();

  public static ApplicationRegistry userRegistry = null; // Initialized in Main()

  public static License GetLicense(bool swallowException = true)
  {
    if (LicenseLive.Value == null && File.Exists(_license_path))
    {
      var lines = File.ReadAllLines(_license_path).ToList();
      try
      {
        LicenseLive.Value = Decrypt(lines);
      }
      catch (Exception)
      {
        if (!swallowException) throw;
      }
    }

    return LicenseLive.Value;
  }

  public static License SetLicense(List<string> lines)
  {
    LicenseLive.Value = Decrypt(lines);
    File.WriteAllLines(_license_path, lines);
    return LicenseLive.Value;
  }

  public static License Decrypt(List<string> lines)
  {
    var encryptedText = lines.Count > 1 ? Symmetric.RemoveTags(lines) : lines.First();
    var cipher = new Cipher(Convert.FromBase64String(encryptedText));
    var symmetric = new Symmetric(Resource.key);
    var decryptedBytes = symmetric.Decrypt(cipher);
    var plainText = Encoding.UTF8.GetString(decryptedBytes);
    return Utilities.Deserialize<object>(plainText, new Type[] { typeof(License) }) as License;
  }

  public static void CheckForUpdate(IWin32Window owner, bool showOnlyIfExists = true)
  {
    try
    {
      var license = Global.GetLicense();
      if (license == null) return;

      var exePath = Assembly.GetExecutingAssembly().Location;
      var exeFolder = Path.GetDirectoryName(exePath);

      var sb = new StringBuilder();
      sb.Append($" /i {license.ApplicationID}");
      sb.Append($" /a \"{exePath}\"");
      sb.Append($" /p {license.Secret}");
      sb.Append($" /e {license.ServiceEndpoint}");

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
    return Assembly.GetExecutingAssembly().GetName().Version?.let(version =>
    {
      var values = version.ToString().Split('.').Take(3);
      return String.Join(".", values);
    }) ?? "0.0.0";
  }
}