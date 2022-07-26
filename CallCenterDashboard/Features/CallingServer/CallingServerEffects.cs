using Azure.Communication.CallingServer;
using CallCenterDashboard.Features.ActiveCalls;
using CallCenterDashboard.Models;
using Fluxor;
using MudBlazor;

namespace CallCenterDashboard.Features.CallingServer;

public class CallingServerEffects
{
    private readonly CallingServerClient _callingServerClient;

    public CallingServerEffects(CallingServerClient callingServerClient)
    {
        _callingServerClient = callingServerClient;
    }

    [EffectMethod]
    public async Task OnHangUp(CallingServerHangUpAction action, IDispatcher dispatcher)
    {
        NotificationData notification;

        try
        {
            var result = await _callingServerClient.GetCallConnection(action.CallData.ConnectionId).HangupAsync(false);
            notification = new NotificationData(result.ReasonPhrase, "Hang up", Severity.Success);
            dispatcher.Dispatch(new ActiveCallsRemoveAction(action.CallData));
        }
        catch (Exception e)
        {
            notification = new NotificationData(e.Message, nameof(CallingServerHangUpAction), Severity.Error);
        }

        dispatcher.Dispatch(new CallingServerNotifyAction(notification));
    }
}