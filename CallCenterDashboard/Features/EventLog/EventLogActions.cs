using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.EventLog;

public record EventLogInitializeAction;

public record EventLogAddAction(EventLogData eventLogData);

public record EventLogClearAction;