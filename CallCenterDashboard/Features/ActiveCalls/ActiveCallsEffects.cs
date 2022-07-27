using CallCenterDashboard.Interfaces;
using CallCenterDashboard.Models;
using Fluxor;
using MudBlazor;

namespace CallCenterDashboard.Features.ActiveCalls;

public class ActiveCallsEffects
{
    private readonly IRepository<CallData> _callDataRepository;

    public ActiveCallsEffects(IRepository<CallData> callDataRepository)
    {
        _callDataRepository = callDataRepository;
    }

    [EffectMethod(typeof(ActiveCallsInitializeAction))]
    public Task OnInitialize(IDispatcher dispatcher)
    {
        var allCallData = _callDataRepository.List();
        dispatcher.Dispatch(new ActiveCallsSetDataAction(allCallData));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task OnAdd(ActiveCallsAddAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ActiveCallsNotifyAction(new NotificationData($"Call {action.CallData.CallConnectionId} connected", "Answered", Severity.Success)));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task OnRemove(ActiveCallsRemoveAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new ActiveCallsNotifyAction(new NotificationData($"Call {action.CallConnectionId} disconnected", "Disconnected", Severity.Success)));
        return Task.CompletedTask;
    }
}