using CallCenterDashboard.Models;
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

    [ReducerMethod(typeof(EventLogInitializeAction))]
    public static EventLogState Initialize(EventLogState state) =>
        state with
        {
            Initialized = true
        };

    [ReducerMethod(typeof(EventLogClearAction))]
    public static EventLogState Clear(EventLogState state) =>
        state with
        {
            EventLogData = Array.Empty<EventLogData>()
        };
}