namespace CallingDashboard.Models;

public record CallData(string From, string To, DateTimeOffset CallStartTime, string CallConnectionId, string CorrelationId, string Id);