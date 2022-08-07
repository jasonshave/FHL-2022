using Azure.Communication;
using CallCenterDashboard.Models;

namespace CallCenterDashboard.Interfaces;

public interface IApplicationSettingsService
{
    ApplicationSettings GetSettings();

    Task<IEnumerable<PhoneNumberConfiguration>> ListPhoneNumberConfigurations();

    Task<CommunicationUserIdentifier> CreateApplicationIdentity(string applicationName);

    void SetApplicationName(string applicationName);

    void SetPhoneNumberConfiguration(PhoneNumberConfiguration phoneNumberConfiguration);
}