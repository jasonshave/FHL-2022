using CallingDashboard.Models;

namespace CallingDashboard.Features.EventLog;

public record EventLogInitializeAction;

public record EventLogSetDataAction(IEnumerable<EventLogData> EventLogData);

public record EventLogAddAction(EventLogData EventLogData);

public record EventLogClearAction;