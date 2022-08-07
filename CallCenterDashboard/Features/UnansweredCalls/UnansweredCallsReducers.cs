using Fluxor;

namespace CallCenterDashboard.Features.UnansweredCalls;

public static class UnansweredCallsReducers
{
    [ReducerMethod]
    public static UnansweredCallsState OnInitialized(UnansweredCallsState state, UnansweredCallsInitializeAction action) =>
        state with
        {
            Initialized = true
        };

    [ReducerMethod]
    public static UnansweredCallsState OnAdd(UnansweredCallsState state, UnansweredCallsAddAction action)
    {
        var existingCalls = state.UnansweredCalls.ToList();
        existingCalls.Add(action.UnansweredCall);

        return state with
        {
            UnansweredCalls = existingCalls
        };
    }

    [ReducerMethod]
    public static UnansweredCallsState OnRemove(UnansweredCallsState state, UnansweredCallsRemoveAction action) =>
        state with
        {
            UnansweredCalls = state.UnansweredCalls.Where(x =>
                x.IncomingCall.IncomingCallContext != action.UnansweredCall.IncomingCall.IncomingCallContext)
        };
}