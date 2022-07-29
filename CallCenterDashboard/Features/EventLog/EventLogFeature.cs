using CallCenterDashboard.Models;
using Fluxor;

namespace CallCenterDashboard.Features.EventLog;

public class EventLogFeature : Feature<EventLogState>
{
    public override string GetName() => "EventLog";

    protected override EventLogState GetInitialState() => new (false, Array.Empty<EventLogData>());
}