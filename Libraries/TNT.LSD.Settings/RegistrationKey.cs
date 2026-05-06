using System.ComponentModel;

namespace TNT.LSD.Settings;

public class RegistrationKey(Guid applicationID, Guid licenseID, string secret, string serviceEndpoint)
{
    [DisplayName("Application ID")]
    public Guid ApplicationID { get; set; } = applicationID;

    [DisplayName("License ID")]
    public Guid LicenseID { get; set; } = licenseID;

    public string Secret { get; set; } = secret;

    [DisplayName("Service Endpoint")]
    public string ServiceEndpoint { get; set; } = serviceEndpoint;

    public RegistrationKey() : this(Guid.Empty, Guid.Empty, string.Empty, string.Empty)
    {
    }
}
