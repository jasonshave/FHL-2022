using Fluxor;

namespace CallCenterDashboard.Features.EventLog;

public class EventLogReducers
{
    [ReducerMethod]
    public static EventLogState Add(EventLogState state, EventLogAddAction action)
    {
        var currentData = state.EventLogData.ToList();
        currentData.Add(action.eventLogData);

        return state with
        {
            EventLogData = currentData
        };
    }
}