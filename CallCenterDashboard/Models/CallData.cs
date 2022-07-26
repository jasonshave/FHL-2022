namespace CallCenterDashboard.Models;

public record CallData(string From, string To, DateTimeOffset CallStartTime, string ConnectionId, string CorrelationId);