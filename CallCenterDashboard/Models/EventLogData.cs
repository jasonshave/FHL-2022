﻿namespace CallingDashboard.Models
{
    public record EventLogData(string EventName, DateTimeOffset EventDateTime, string? CallConnectionId, string? CorrelationId);
}
