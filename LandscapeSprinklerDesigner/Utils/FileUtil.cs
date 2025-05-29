using System.Text.Json;
using TNT.Commons;
using TNT.Cryptography;
using TNT.LSD.Settings;
using TNT.Services.Models.Dto;

namespace LandscapeSprinklerDesigner.Utils;

internal static class FileUtil
{
  private const string REGISTRATION_KEY_FILE = "Registration.key";
  private const string LICENSE_FILE = "License.txt";

  private static SymmetricCipher? Cipher = JsonSerializer.Deserialize<CipherAttributes>(Resource.cipher_attributes)?.Let(attrs => new SymmetricCipher(attrs));

  public static RegistrationKey? GetRegistrationKey()
  {
    if (!File.Exists(REGISTRATION_KEY_FILE) || Cipher == null) return null;

    var registrationKey = File.ReadAllLines(REGISTRATION_KEY_FILE).ToList();
    var unformatedRegistrationKey = FormatUtils.RemoveTags(registrationKey);

    return Cipher.Decrypt<RegistrationKey>(unformatedRegistrationKey);
  }

  public static void SaveRegistrationKey(List<String> registrationKey) => File.WriteAllLines(REGISTRATION_KEY_FILE, registrationKey);

  public static void SaveRegistrationKey(RegistrationKey registrationKey)
  {
    if (Cipher == null) return;

    var registrationKeyJson = JsonSerializer.Serialize(registrationKey);
    var encryptedRegistrationKey = Cipher.Encrypt(registrationKeyJson);
    File.WriteAllLines(REGISTRATION_KEY_FILE, FormatUtils.FormatWithTags(encryptedRegistrationKey));
  }

  public static LicenseeInfoDto? GetLicenseeInfo()
  {
    if (!File.Exists(LICENSE_FILE) || Cipher == null) return null;

    var licenseJson = File.ReadAllText(LICENSE_FILE);
    return Cipher.Decrypt<LicenseeInfoDto>(licenseJson);
  }

  public static void SaveLicenseInfo(LicenseeInfoDto licenseInfo)
  {
    if (Cipher == null) return;
    File.WriteAllText(LICENSE_FILE, Cipher.Encrypt(licenseInfo));
  }

  public static bool HasValidLicense() => DateTime.Now < FileUtil.GetLicenseeInfo()?.ValidUntil.DateTime;
}
