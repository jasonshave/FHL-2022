using Azure.Communication.CallingServer;
using CallCenterDashboard.Interfaces;
using CallCenterDashboard.Models;
using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

namespace CallCenterDashboard.Services;

public class IncomingCallHandler : BackgroundService
{
    private readonly IRepository<CallData> _callDataRepository;
    private readonly ICallingServerEventSubscriber _callingServerEventSubscriber;
    private readonly CallingServerClient _callingServerClient;
    private readonly IConfiguration _configuration;

    public IncomingCallHandler(
        IRepository<CallData> callDataRepository,
        ICallingServerEventSubscriber callingServerEventSubscriber, 
        CallingServerClient callingServerClient, 
        IConfiguration configuration)
    {
        _callDataRepository = callDataRepository;
        _callingServerEventSubscriber = callingServerEventSubscriber;
        _callingServerClient = callingServerClient;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _callingServerEventSubscriber.OnIncomingCall += HandleIncomingCall;
        _callingServerEventSubscriber.OnCallConnected += HandleCallConnected;

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async ValueTask HandleIncomingCall(IncomingCall incomingCall, string? contextId)
    {
        if (incomingCall.To.RawId != _configuration["ACS:TargetId"]) return;

        var baseUri = _configuration["ACS:CallbackUri"];
        var callbackUri = new Uri($"{baseUri}/api/calls/{incomingCall.CorrelationId}");

        AnswerCallResult result = await _callingServerClient.AnswerCallAsync(incomingCall.IncomingCallContext, callbackUri);
        _callDataRepository.Add(result.CallProperties.CallConnectionId, new CallData(incomingCall.From.RawId, incomingCall.To.RawId, DateTimeOffset.UtcNow, result.CallProperties.CallConnectionId, incomingCall.CorrelationId));
    }

    private async ValueTask HandleCallConnected(CallConnected callConnected, string? contextId)
    {
        await _callingServerClient.GetCallConnection(callConnected.CallConnectionId)
            .GetCallMedia()
            .PlayToAllAsync(
                new FileSource(new Uri("https://acstestapp1.azurewebsites.net/audio/bot-hold-music-2.wav")));
    }
}