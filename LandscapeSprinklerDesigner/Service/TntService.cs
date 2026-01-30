using LandscapeSprinklerDesigner.Utils;
using TNT.Commons;
using TNT.LSD.Settings;
using TNT.Reactive;
using TNT.Services.Client;
using TNT.Services.Models;
using TNT.Services.Models.Dto;

namespace LandscapeSprinklerDesigner.Service;

public static class TntService
{
    private static AuthenticatedClient? _AuthClient = null;

    private static RegistrationKey? RegistrationKey => FileUtil.GetRegistrationKey();

    private static AuthenticatedClient? AuthClient
    {
        get
        {
            if (_AuthClient == null && RegistrationKey != null)
            {
                var baseUri = new Uri(RegistrationKey.ServiceEndpoint);
                var client = new Client(baseUri);

                var jwtResponse = client.Authorize(RegistrationKey.ApplicationID, RegistrationKey.Secret);
                if (jwtResponse.IsSuccess)
                {
                    _AuthClient = jwtResponse.Data?.Let(data => new AuthenticatedClient(baseUri, jwtResponse.Data));
                }
            }

            return _AuthClient;
        }
    }

    public static DtoResponse<LicenseeInfoDto>? GetLicenseInfoResponse()
    {
        if (RegistrationKey == null || AuthClient == null) return null;
        return AuthClient.LicenseeInfo(RegistrationKey.ApplicationID, RegistrationKey.LicenseID);
    }

    public static LicenseeInfoDto? GetLicenseeInfo()
    {
        if (RegistrationKey == null || AuthClient == null) return null;

        var licenseeInfoResponse = AuthClient.LicenseeInfo(RegistrationKey.ApplicationID, RegistrationKey.LicenseID);
        LicenseeInfoDto? licenseeInfo = null;

        if (licenseeInfoResponse.IsSuccess)
        {
            licenseeInfo = licenseeInfoResponse.Data;
        }
        else
        {
            licenseeInfo = FileUtil.GetLicenseeInfo();
        }

        return licenseeInfoResponse.Data;
    }

    public static Flow<LicenseeInfoDto?> GetLicenseeInfoFlow()
    {
        var licenseeInfo = FileUtil.GetLicenseeInfo();
        var licenseInfoDtoFlow = new MutableStateFlow<LicenseeInfoDto?>(licenseeInfo);

        Task.Run(() =>
        {
            if (RegistrationKey != null && AuthClient != null)
            {
                var licenseInfoResponse = AuthClient.LicenseeInfo(RegistrationKey.ApplicationID, RegistrationKey.LicenseID);
                if (licenseInfoResponse.IsSuccess)
                {
                    licenseInfoDtoFlow.value = licenseInfoResponse.Data;
                }
            }
        });

        return licenseInfoDtoFlow;
    }
}
