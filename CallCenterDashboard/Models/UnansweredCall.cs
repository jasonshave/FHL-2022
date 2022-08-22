using JasonShave.Azure.Communication.Service.CallAutomation.Sdk.Contracts;

namespace CallingDashboard.Models;

public record UnansweredCall(IncomingCall IncomingCall, string? ContextId, DateTimeOffset CallStart);