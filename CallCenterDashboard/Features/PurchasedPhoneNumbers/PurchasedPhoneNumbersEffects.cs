using Azure.Communication.PhoneNumbers;
using CallCenterDashboard.Interfaces;
using CallCenterDashboard.Models;
using Fluxor;

namespace CallCenterDashboard.Features.PurchasedPhoneNumbers;

public class PurchasedPhoneNumbersEffects
{
    private readonly PhoneNumbersClient _phoneNumbersClient;
    private readonly IApplicationSettingsService _applicationSettingsService;

    public PurchasedPhoneNumbersEffects(
        PhoneNumbersClient phoneNumbersClient,
        IApplicationSettingsService applicationSettingsService)
    {
        _phoneNumbersClient = phoneNumbersClient;
        _applicationSettingsService = applicationSettingsService;
    }

    [EffectMethod(typeof(PurchasedPhoneNumbersInitializeAction))]
    public async Task OnInitialize(IDispatcher dispatcher)
    {
        var configurations = new List<PhoneNumberConfiguration>();
        var result = _phoneNumbersClient.GetPurchasedPhoneNumbersAsync();
        await foreach (var purchasedPhoneNumbers in result.AsPages())
        {
            configurations.AddRange(purchasedPhoneNumbers.Values.Select(purchasedPhoneNumber => 
                new PhoneNumberConfiguration { PhoneNumber = purchasedPhoneNumber }));
        }
        dispatcher.Dispatch(new PurchasedPhoneNumbersSetDataAction(configurations));
        dispatcher.Dispatch(new PurchasedPhoneNumbersSetLoadedAction());
    }

    [EffectMethod]
    public async Task OnSetData(PurchasedPhoneNumbersSetDataAction action, IDispatcher dispatcher)
    {
        foreach (var phoneNumberConfiguration in action.PhoneNumbers)
        {
            _applicationSettingsService.SetPhoneNumberConfiguration(phoneNumberConfiguration);
        }
    }
}