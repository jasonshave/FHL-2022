using CallingDashboard.Models;

namespace CallingDashboard.Features.EventLog;

public record EventLogState(bool Initialized, IEnumerable<EventLogData> EventLogData);