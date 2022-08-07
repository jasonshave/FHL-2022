using CallingDashboard.Interfaces;
using CallingDashboard.Models;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;
using MudBlazor;

namespace CallingDashboard.Features.ActiveCalls;

public class ActiveCallsEffects
{
    private readonly ICallingServerEventSubscriber _callingServerEventSubscriber;
    private readonly IRepository<CallData> _callDataRepository;

    public ActiveCallsEffects(
        ICallingServerEventSubscriber callingServerEventSubscriber,
        IRepository<CallData> callDataRepository)
    {
        _callingServerEventSubscriber = callingServerEventSubscriber;
        _callDataRepository = callDataRepository;
    }

    [EffectMethod(typeof(ActiveCallsInitializeAction))]
    public Task OnInitialize(IDispatcher dispatcher)
    {
        _callingServerEventSubscriber.OnCallConnected += (@event, contextId) =>
        {
            var callData = _callDataRepository.Find(contextId);
            if (callData is not null) dispatcher.Dispatch(new ActiveCallsAddAction(callData));

            return ValueTask.CompletedTask;
        };

        _callingServerEventSubscriber.OnCallDisconnected += (@event, contextId) =>
        {
            _callDataRepository.Remove(contextId);
            dispatcher.Dispatch(new ActiveCallsRemoveAction(contextId));
            return ValueTask.CompletedTask;
        };

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
        dispatcher.Dispatch(new ActiveCallsNotifyAction(new NotificationData($"Call {action.Id} disconnected", "Disconnected", Severity.Success)));
        return Task.CompletedTask;
    }
}