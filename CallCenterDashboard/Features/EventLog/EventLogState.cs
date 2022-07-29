using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.EventLog;

public record EventLogState(bool Initialized, IEnumerable<EventLogData> EventLogData);