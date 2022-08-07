using Azure.Communication;
using Azure.Communication.Identity;
using Azure.Communication.PhoneNumbers;
using CallingDashboard.Interfaces;
using CallingDashboard.Models;

namespace CallingDashboard.Services;

public class ApplicationSettingsService : IApplicationSettingsService
{
    private readonly CommunicationIdentityClient _identityClient;
    private readonly PhoneNumbersClient _phoneNumbersClient;
    private readonly ApplicationSettings _applicationSettings = new();

    public ApplicationSettingsService(
        CommunicationIdentityClient identityClient,
        PhoneNumbersClient phoneNumbersClient)
    {
        _identityClient = identityClient;
        _phoneNumbersClient = phoneNumbersClient;
    }

    public ApplicationSettings GetSettings() => _applicationSettings;

    public async Task<IEnumerable<PhoneNumberConfiguration>> ListPhoneNumberConfigurations()
    {
        var result = _phoneNumbersClient.GetPurchasedPhoneNumbersAsync();
        if (result is null) return Array.Empty<PhoneNumberConfiguration>();

        var phoneNumberConfigurations = new HashSet<PhoneNumberConfiguration>();
        var existingConfigurations = _applicationSettings.PhoneNumbers;
        await foreach (var purchasedPhoneNumbersPage in result.AsPages())
        {
            foreach (var purchasedPhoneNumber in purchasedPhoneNumbersPage.Values)
            {
                // check to see if we have a configuration for this number already
                PhoneNumberConfiguration? item = existingConfigurations.FirstOrDefault(x => x.PhoneNumber.Id == purchasedPhoneNumber.Id);
                phoneNumberConfigurations.Add(item ?? new PhoneNumberConfiguration
                {
                    Id = purchasedPhoneNumber.Id,
                    PhoneNumber = purchasedPhoneNumber
                });
            }
        }

        return phoneNumberConfigurations;
    }

    public void SetPhoneNumberConfiguration(PhoneNumberConfiguration phoneNumberConfiguration)
    {
        // check to see if it exists
        var configuration = _applicationSettings.PhoneNumbers.FirstOrDefault(x => x.Id == phoneNumberConfiguration.Id);
        if (configuration is not null)
        {
            // change it
            configuration.AutoAnswer = phoneNumberConfiguration.AutoAnswer;
        }
        else
        {
            // add it
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