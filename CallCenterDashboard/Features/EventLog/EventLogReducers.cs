using CallCenterDashboard.Models;
using Fluxor;

namespace CallCenterDashboard.Features.EventLog;

public class EventLogReducers
{
    [ReducerMethod]
    public static EventLogState Add(EventLogState state, EventLogAddAction action)
    {
        var currentData = state.EventLogData.ToList();
        currentData.Add(action.EventLogData);

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

    [ReducerMethod]
    public static EventLogState OnSetData(EventLogState state, EventLogSetDataAction action) =>
        state with
        {
            EventLogData = action.EventLogData
        };

    [ReducerMethod(typeof(EventLogClearAction))]
    public static EventLogState Clear(EventLogState state) =>
        state with
        {
            EventLogData = Array.Empty<EventLogData>()
        };
}