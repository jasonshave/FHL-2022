using Fluxor;

namespace CallCenterDashboard.Features.CallingServer;

public static class CallingServerReducers
{
    [ReducerMethod(typeof(CallingServerHangUpAction))]
    public static CallingServerState OnHangUpAction(CallingServerState state) =>
        state with
        {
            Waiting = true
        };

    [ReducerMethod(typeof(CallingServerNotifyAction))]
    public static CallingServerState OnNotifyAction(CallingServerState state) =>
        state with
        {
            Waiting = false
        };
}