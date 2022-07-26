using Fluxor;

namespace CallCenterDashboard.Features.ActiveCalls;

public static class ActiveCallsReducers
{
    [ReducerMethod]
    public static ActiveCallsState OnSetDataAction(ActiveCallsState state, ActiveCallsSetDataAction action) =>
        state with
        {
            CallData = action.CallData,
            Loading = false
        };

    [ReducerMethod(typeof(ActiveCallsLoadDataAction))]
    public static ActiveCallsState OnLoadDataAction(ActiveCallsState state) => state with { Loading = true };

    [ReducerMethod(typeof(ActiveCallsSetInitializedAction))]
    public static ActiveCallsState OnSetInitializedAction(ActiveCallsState state) => state with { Initialized = true };
}