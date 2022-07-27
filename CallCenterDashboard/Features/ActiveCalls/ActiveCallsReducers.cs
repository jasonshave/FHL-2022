using CallCenterDashboard.Models;
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

    [ReducerMethod]
    public static ActiveCallsState OnAddDataAction(ActiveCallsState state, ActiveCallsAddAction action)
    {
        var newList = new List<CallData>(state.CallData);
        
        if (!newList.Contains(action.CallData))
        {
            newList.Add(action.CallData);
        }

        return state with
        {
            CallData = newList
        };
    }

    [ReducerMethod]
    public static ActiveCallsState OnRemoveDataAction(ActiveCallsState state, ActiveCallsRemoveAction action) =>
        state with
        {
            CallData = state.CallData.Where(x => x.CallConnectionId != action.CallConnectionId)
        };

    [ReducerMethod(typeof(ActiveCallsLoadDataAction))]
    public static ActiveCallsState OnLoadDataAction(ActiveCallsState state) => state with { Loading = true };

    [ReducerMethod(typeof(ActiveCallsSetInitializedAction))]
    public static ActiveCallsState OnSetInitializedAction(ActiveCallsState state) => state with { Initialized = true };
}