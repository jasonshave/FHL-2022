using CallCenterDashboard.Interfaces;
using Fluxor;

namespace CallCenterDashboard.Features.ApplicationIdentity;

public class ApplicationIdentityEffects
{
    private readonly IApplicationSettingsService _applicationSettingsService;

    public ApplicationIdentityEffects(IApplicationSettingsService applicationSettingsService)
    {
        _applicationSettingsService = applicationSettingsService;
    }

    [EffectMethod(typeof(ApplicationIdentityInitializeAction))]
    public Task OnInitialize(IDispatcher dispatch)
    {
        var name = _applicationSettingsService.GetSettings().ApplicationName;
        if (name is not null) dispatch.Dispatch(new ApplicationIdentitySetNameAction(name));
        
        var id = _applicationSettingsService.GetSettings().ApplicationIdentity;
        if(id is not null) dispatch.Dispatch(new ApplicationIdentitySetIdentityAction(id));

        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task OnSetName(ApplicationIdentitySetNameAction action, IDispatcher dispatcher)
    {
        _applicationSettingsService.SetApplicationName(action.ApplicationName);
        return Task.CompletedTask;
    }

    [EffectMethod]
    public async Task OnSetIdentity(ApplicationIdentitySetNameAction action, IDispatcher dispatcher)
    {
        var identity = await _applicationSettingsService.CreateApplicationIdentity(action.ApplicationName);
        dispatcher.Dispatch(new ApplicationIdentitySetIdentityAction(identity));
    }
}