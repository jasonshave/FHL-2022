using CallingDashboard.Models;
using Fluxor;

namespace CallingDashboard.Features.EventLog;

public class EventLogFeature : Feature<EventLogState>
{
    public override string GetName() => "EventLog";

    protected override EventLogState GetInitialState() => new(false, Array.Empty<EventLogData>());
}