using Azure.Communication;
using Azure.Communication.Identity;
using CallCenterDashboard.Interfaces;
using CallCenterDashboard.Models;

namespace CallCenterDashboard.Services;

public class ApplicationSettingsService : IApplicationSettingsService
{
    private readonly CommunicationIdentityClient _identityClient;
    private readonly ApplicationSettings _applicationSettings = new();

    public ApplicationSettingsService(CommunicationIdentityClient identityClient)
    {
        _identityClient = identityClient;
    }

    public ApplicationSettings GetSettings() => _applicationSettings;

    public IEnumerable<PhoneNumberConfiguration> ListPhoneNumbers() => _applicationSettings.PhoneNumbers;

    public void SetPhoneNumberConfiguration(PhoneNumberConfiguration phoneNumberConfiguration)
    {
        // check to see if it exists
        var exists = _applicationSettings.PhoneNumbers.Contains(phoneNumberConfiguration);
        if (exists)
        {
            var configuration = _applicationSettings.PhoneNumbers.FirstOrDefault(x => x.PhoneNumber.Id == phoneNumberConfiguration.PhoneNumber.Id);
            if (configuration is not null) configuration.AutoAnswer = phoneNumberConfiguration.AutoAnswer;
        }
        else
        {
            _applicationSettings.PhoneNumbers.Add(phoneNumberConfiguration);
        }
    }

    public async Task<CommunicationUserIdentifier> CreateApplicationIdentity(string applicationName)
    {
        var identity = await _identityClient.CreateUserAsync();
        _applicationSettings.ApplicationIdentity = identity.Value;
        _applicationSettings.ApplicationName = applicationName;
        return identity;
    }

    public void SetApplicationName(string applicationName) => _applicationSettings.ApplicationName = applicationName;
}