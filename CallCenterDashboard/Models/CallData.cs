using Azure.Communication.CallingServer;

namespace CallCenterDashboard.Models;

public record CallData(string From, string To, TimeSpan CallDuration, string ConnectionId, string CorrelationId);