using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

namespace CallingDashboard.Models;

public record UnansweredCall(IncomingCall IncomingCall, string? ContextId, DateTimeOffset CallStart);