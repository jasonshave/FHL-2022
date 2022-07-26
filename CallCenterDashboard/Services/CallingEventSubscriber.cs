using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

namespace CallCenterDashboard.Services;

public class CallingEventSubscriber : BackgroundService
{
    private readonly ICallingServerEventSubscriber _callingServerEventSubscriber;

    public CallingEventSubscriber(ICallingServerEventSubscriber callingServerEventSubscriber)
    {
        _callingServerEventSubscriber = callingServerEventSubscriber;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // todo: put subscription logic here
        _callingServerEventSubscriber.OnIncomingCall += HandleIncomingCall;

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private ValueTask HandleIncomingCall(IncomingCall incomingCall, string? contextId) => ValueTask.CompletedTask;
}