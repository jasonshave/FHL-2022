﻿using Azure;
using Azure.Communication.CallingServer;
using CallingDashboard.Features.ActiveCalls;
using CallingDashboard.Features.UnansweredCalls;
using CallingDashboard.Interfaces;
using CallingDashboard.Models;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;
using MudBlazor;

namespace CallingDashboard.Features.CallingServer;

public class CallingServerEffects
{
    private readonly IRepository<CallData> _callDataRepository;
    private readonly ICallAutomationEventSubscriber _callAutomationEventSubscriber;
    private readonly CallAutomationClient _callingServerClient;
    private readonly string _callbackUri;

    public CallingServerEffects(
        IRepository<CallData> callDataRepository,
        ICallAutomationEventSubscriber callAutomationEventSubscriber,
        CallAutomationClient callingServerClient,
        IConfiguration configuration)
    {
        _callDataRepository = callDataRepository;
        _callAutomationEventSubscriber = callAutomationEventSubscriber;
        _callingServerClient = callingServerClient;
        _callbackUri = configuration["ACS:CallbackUri"];
    }

    [EffectMethod]
    public async Task OnReject(CallingServerRejectAction action, IDispatcher dispatcher)
    {
        try
        {
            await _callingServerClient.RejectCallAsync(action.UnansweredCall.IncomingCall.IncomingCallContext, CallRejectReason.None);
            var notification = new NotificationData("Call rejected.", nameof(CallingServerRejectAction), Severity.Warning);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData(e.Message, nameof(CallingServerRejectAction), Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }

    [EffectMethod]
    public async Task OnHangUp(CallingServerHangUpAction action, IDispatcher dispatcher)
    {
        try
        {
            await _callingServerClient.GetCallConnection(action.CallData.CallConnectionId).HangUpAsync(false);
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData("Hang up failed.", nameof(CallingServerHangUpAction), Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }

    [EffectMethod]
    public async Task OnTerminate(CallingServerTerminateAction action, IDispatcher dispatcher)
    {
        try
        {
            await _callingServerClient.GetCallConnection(action.CallData.CallConnectionId).HangUpAsync(true);
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
            AddParticipantsResult result = await _callingServerClient.GetCallConnection(action.CallData.CallConnectionId)
                .AddParticipantsAsync(new[] { action.UserToAdd });
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData("Add participant failed.", nameof(CallingServerAddParticipantAction), Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }

    [EffectMethod]
    public async Task OnRemoveParticipant(CallingServerRemoveParticipantAction action, IDispatcher dispatcher)
    {
        try
        {
            RemoveParticipantsResult result = await _callingServerClient.GetCallConnection(action.CallData.CallConnectionId)
                .RemoveParticipantsAsync(new[] { action.UserToRemove });
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData("Remove participant failed.", nameof(CallingServerRemoveParticipantAction), Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }

    [EffectMethod]
    public async Task OnTransferCall(CallingServerTransferCallAction action, IDispatcher dispatcher)
    {
        try
        {
            TransferCallToParticipantResult result = await _callingServerClient.GetCallConnection(action.CallData.CallConnectionId)
                .TransferCallToParticipantAsync(action.UserToTransferTo);
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData("Transfer call failed.", nameof(CallingServerTransferCallAction), Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }

    [EffectMethod]
    public async Task OnPlayAudio(CallingServerPlayAudioAction action, IDispatcher dispatcher)
    {
        try
        {
            Response? response = await _callingServerClient.GetCallConnection(action.CallData.CallConnectionId)
                .GetCallMedia()
                .PlayToAllAsync(new FileSource(action.AudioLocation), new PlayOptions() { Loop = action.Loop });

            if (response.IsError)
            {
                var notification = new NotificationData(response.ReasonPhrase, nameof(CallingServerPlayAudioAction),
                    Severity.Error);
                dispatcher.Dispatch(new CallingServerNotifyAction(notification));
            }
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData(e.Message, nameof(CallingServerPlayAudioAction),
                Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }

    [EffectMethod]
    public async Task OnCreateCall(CallingServerCreateCallAction action, IDispatcher dispatcher)
    {
        try
        {
            var id = Guid.NewGuid().ToString();
            var callbackUri = $"{_callbackUri}/api/calls/{id}";

            CallSource callSource = new CallSource(action.Source);

            if (action.AlternateCallerId is not null) callSource.CallerId = action.AlternateCallerId;

            CreateCallResult createCallResult = await _callingServerClient.CreateCallAsync(callSource, action.Targets, new Uri(callbackUri));

            var callData = new CallData(
                From: createCallResult.CallConnectionProperties.CallSource.Identifier.RawId,
                To: createCallResult.CallConnectionProperties.Targets[0].RawId,
                DateTimeOffset.UtcNow,
                createCallResult.CallConnectionProperties.CallConnectionId,
                null,
                id);
            _callDataRepository.Add(id, callData);

            dispatcher.Dispatch(new ActiveCallsAddAction(callData));
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData(e.Message, nameof(CallingServerCreateCallAction),
                Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }

    [EffectMethod]
    public async Task OnAnswerCall(CallingServerAnswerAction action, IDispatcher dispatcher)
    {
        try
        {
            var id = Guid.NewGuid().ToString();
            var callbackUri = new Uri($"{_callbackUri}/api/calls/{id}");
            AnswerCallResult answerCallResult = await _callingServerClient.AnswerCallAsync(action.UnansweredCall.IncomingCall.IncomingCallContext, callbackUri);
            _callDataRepository.Add(id,
                new CallData(action.UnansweredCall.IncomingCall.From.RawId,
                    action.UnansweredCall.IncomingCall.To.RawId,
                    DateTimeOffset.UtcNow,
                    answerCallResult.CallConnectionProperties.CallConnectionId,
                    action.UnansweredCall.IncomingCall.CorrelationId,
                    id));

            dispatcher.Dispatch(new UnansweredCallsRemoveAction(action.UnansweredCall));
        }
        catch (RequestFailedException e)
        {
            var notification = new NotificationData(e.Message, nameof(CallingServerAnswerAction),
                Severity.Error);
            dispatcher.Dispatch(new CallingServerNotifyAction(notification));
        }
    }
}