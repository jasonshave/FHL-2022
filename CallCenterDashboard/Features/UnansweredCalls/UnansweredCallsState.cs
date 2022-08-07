using CallingDashboard.Models;

namespace CallingDashboard.Features.UnansweredCalls;

public record UnansweredCallsState(bool Initialized, IEnumerable<UnansweredCall> UnansweredCalls);