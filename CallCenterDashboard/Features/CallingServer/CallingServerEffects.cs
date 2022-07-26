using Azure;
using Azure.Communication;
using Azure.Communication.CallingServer;
using CallCenterDashboard.Features.ActiveCalls;
using CallCenterDashboard.Interfaces;
using CallCenterDashboard.Models;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;
using MudBlazor;

namespace CallCenterDashboard.Features.CallingServer;

public class CallingServerEffects
{
    private readonly IRepository<CallData> _callDataRepository;
    private readonly ICallingServerEventSubscriber _callingServerEventSubscriber;
    private readonly CallingServerClient _callingServerClient;

    public CallingServerEffects(
        IRepository<CallData> callDataRepository,
        ICallingServerEventSubscriber callingServerEventSubscriber,
        CallingServerClient callingServerClient)
    {
        _callDataRepository = callDataRepository;
        _callingServerEventSubscriber = callingServerEventSubscriber;
        _callingServerClient = callingServerClient;
    }

    [EffectMethod]
    public async Task OnInitialize(CallingServerInitializeAction action, IDispatcher dispatcher)
    {
        _callingServerEventSubscriber.OnCallConnected += (@event, contextId) =>
        {
            var callData = _callDataRepository.Get(@event.CallConnectionId);
            dispatcher.Dispatch(new ActiveCallsAddAction(callData));
            dispatcher.Dispatch(new CallingServerNotifyAction(new NotificationData($"Call {@event.CallConnectionId} connected", "Answered", Severity.Success)));
            
            return ValueTask.CompletedTask;
        };

        _callingServerEventSubscriber.OnCallDisconnected += (@event, contextId) =>
        {
            _callDataRepository.Remove(@event.CallConnectionId);
            dispatcher.Dispatch(new ActiveCallsRemoveAction(@event.CallConnectionId));
            dispatcher.Dispatch(new CallingServerNotifyAction(new NotificationData($"Call {@event.CallConnectionId} disconnected", "Hang up", Severity.Success)));

            return ValueTask.CompletedTask;
        };
    }

    [EffectMethod]
    public async Task OnHangUp(CallingServerHangUpAction action, IDispatcher dispatcher)
    {
        NotificationData notification;

        try
        {
            await _callingServerClient.GetCallConnection(action.CallData.ConnectionId).HangupAsync(false);
        }
        catch (RequestFailedException e)
        {
            notification = new NotificationData("Hang up failed.", nameof(CallingServerHangUpAction), Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }

    [EffectMethod]
    public async Task OnTerminate(CallingServerTerminateAction action, IDispatcher dispatcher)
    {
        try
        {
            await _callingServerClient.GetCallConnection(action.CallData.ConnectionId).HangupAsync(true);
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData("Terminate call failed.", nameof(CallingServerHangUpAction), Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }

    [EffectMethod]
    public async Task OnAddParticipant(CallingServerAddParticipantAction action, IDispatcher dispatcher)
    {
        try
        {
            AddParticipantsResult result = await _callingServerClient.GetCallConnection(action.CallData.ConnectionId)
                .AddParticipantsAsync(new []{action.UserToAdd});
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData("Add participant failed.", nameof(CallingServerAddParticipantAction), Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }
}