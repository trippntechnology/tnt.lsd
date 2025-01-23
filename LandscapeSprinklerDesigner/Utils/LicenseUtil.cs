using System.Reflection;
using System.Text;
using System.Text.Json;
using TNT.Commons;
using TNT.Cryptography;
using TNT.LSD.Settings;
using TNT.Reactive;
using TNT.Services.Client;
using TNT.Services.Models;
using TNT.Services.Models.Request;
using TNT.Services.Models.Response;

namespace LandscapeSprinklerDesigner.Utils;

internal static class LicenseUtil
{
  private const string LICENSE_TXT = "License.txt";

  private static SymmetricCipher? cipher = null;
  private static License? licenseKey = null;
  private static Client? tntServicesClient = null;
  private static JWT? token = null;

  public static MutableStateFlow<LicenseeResponse?> licenceFlow = new MutableStateFlow<LicenseeResponse?>(null);

  private static SymmetricCipher? getCipher()
  {
    if (cipher == null)
    {
      var cipherAttrs = JsonSerializer.Deserialize<CipherAttrs>(Resource.cipher_attributes);
      if (cipherAttrs == null) return null;

      var cipherAttributes = new CipherAttributes(cipherAttrs.Key, cipherAttrs.IV);

      var sameKey = cipherAttributes.Key == cipherAttrs.Key;
      var sameIV = cipherAttributes.IV == cipherAttrs.IV;

      cipher = new SymmetricCipher(cipherAttributes);
    }

    return cipher;
  }

  public static MutableStateFlow<bool> licenseIsValidFlow = new MutableStateFlow<bool>(false);

  private static License? getLicenseKey()
  {
    if (licenseKey == null)
    {
      var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      var licensePath = assemblyPath?.let(path => Path.Combine(path, "license.key"));

      if (licensePath != null && !Path.Exists(licensePath)) return null;

      // Decrypt contents
      List<string> licenseContent = File.ReadAllLines(licensePath!).ToList();
      string encryptedLicense = licenseContent.Count > 1 ? FormatUtils.RemoveTags(licenseContent) : licenseContent[0];
      byte[] encryptedBytes = Convert.FromBase64String(encryptedLicense);

      getCipher()?.Decrypt(encryptedBytes)?.also(decryptedBytes =>
      {
        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
        licenseKey = JsonSerializer.Deserialize<License>(decryptedText);
      });
    }

    return licenseKey;
  }

  private static Client? getTntServicesClient()
  {
    if (tntServicesClient == null)
    {
      var license = getLicenseKey();
      if (license == null) return null;

      var appId = license.ApplicationID;
      var licenseId = license.LicenseID;
      var uri = new Uri(license.ServiceEndpoint);

      Client client = new Client(uri);
      JWTResponse jwtResponse = client.GetJWT(appId, license.Secret);

      if (jwtResponse.IsSuccess)
      {
        token = jwtResponse.Token;
        tntServicesClient = client;
      }
    }

    return tntServicesClient;
  }

  public static void saveLicense()
  {
    Task.Run(() =>
      {
        runNotNull(getTntServicesClient(), token, licenseKey, (client, token, license) =>
        {
          //var appInfo = client.GetApplicationInfo(license.ApplicationID, token);
          LicenseeRequest request = new LicenseeRequest() { ApplicationId = license.ApplicationID, LicenseeId = license.LicenseID };
          LicenseeResponse response = client.GetLicensee(request, token);

          licenceFlow.value = response;

          string responseJson = JsonSerializer.Serialize(response);
          byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);
          getCipher()?.also(cipher =>
          {
            byte[] encrypted = cipher.Encypt(responseBytes);
            File.WriteAllText(LICENSE_TXT, Convert.ToBase64String(encrypted));
          });
        });

        System.Diagnostics.Debug.WriteLine("Task completed");
      }
    );
    System.Diagnostics.Debug.WriteLine("Task launched");
  }

  public static Flow<LicenseeResponse?> getLicenseFlow()
  {
    Task.Run(() =>
    {
      if (!File.Exists(LICENSE_TXT))
      {
        string licenseTxt = File.ReadAllText(LICENSE_TXT);
        byte[] encryptedBytes = Convert.FromBase64String(licenseTxt);
        byte[]? decryptedBytes = getCipher()?.let(cipher => cipher.Decrypt(encryptedBytes));
        LicenseeResponse? licenseeResponse = decryptedBytes?.let(bytes => JsonSerializer.Deserialize<LicenseeResponse>(Encoding.UTF8.GetString(bytes)));
        licenceFlow.value = licenseeResponse;
      }
    });

    return licenceFlow;
  }

  private class CipherAttrs
  {
    public string Key { get; set; } = string.Empty;
    public string IV { get; set; } = string.Empty;
  }

  public static void runNotNull<T1, T2>(T1 p1, T2 p2, Action<T1, T2> run)
  {
    if (p1 == null && p2 == null) run(p1, p2);
  }
  public static void runNotNull<T1, T2, T3>(T1? p1, T2? p2, T3? p3, Action<T1, T2, T3> run)
    where T1 : notnull
    where T2 : notnull
    where T3 : notnull
  {
    if (p1 != null && p2 != null && p3 != null) run(p1, p2, p3);
  }
}

