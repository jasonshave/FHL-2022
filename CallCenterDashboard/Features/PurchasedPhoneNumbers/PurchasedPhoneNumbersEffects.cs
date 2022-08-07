using CallCenterDashboard.Interfaces;
using Fluxor;

namespace CallCenterDashboard.Features.PurchasedPhoneNumbers;

public class PurchasedPhoneNumbersEffects
{
    private readonly IApplicationSettingsService _applicationSettingsService;

    public PurchasedPhoneNumbersEffects(IApplicationSettingsService applicationSettingsService)
    {
        _applicationSettingsService = applicationSettingsService;
    }

    [EffectMethod(typeof(PurchasedPhoneNumbersInitializeAction))]
    public async Task OnInitialize(IDispatcher dispatcher)
    {
        var configurations = await _applicationSettingsService.ListPhoneNumberConfigurations();
        configurations.ToList().ForEach(x => dispatcher.Dispatch(new PurchasedPhoneNumbersSetAnswerModeAction(x)));
        dispatcher.Dispatch(new PurchasedPhoneNumbersSetDataAction(configurations));
        dispatcher.Dispatch(new PurchasedPhoneNumbersSetLoadedAction());
    }

    //[EffectMethod]
    //public async Task OnSetData(PurchasedPhoneNumbersSetDataAction action, IDispatcher dispatcher)
    //{
    //    foreach (var phoneNumberConfiguration in action.PhoneNumbers)
    //    {
    //        _applicationSettingsService.SetPhoneNumberConfiguration(phoneNumberConfiguration);
    //    }
    //}

    [EffectMethod]
    public Task OnSetAnswerMode(PurchasedPhoneNumbersSetAnswerModeAction action, IDispatcher dispatcher)
    {
        _applicationSettingsService.SetPhoneNumberConfiguration(action.PhoneNumberConfiguration);
        return Task.CompletedTask;
    }
}