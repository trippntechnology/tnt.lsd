using System.ComponentModel;

namespace TNT.LSD.Settings;

public class RegistrationKey
{
  [DisplayName("Application ID")]
  public Guid ApplicationID { get; set; } = Guid.Empty;

  [DisplayName("License ID")]
  public Guid LicenseID { get; set; } = Guid.Empty;

  public string Secret { get; set; } = string.Empty;

  [DisplayName("Service Endpoint")]
  public string ServiceEndpoint { get; set; } = string.Empty;

  public RegistrationKey() { }
}
