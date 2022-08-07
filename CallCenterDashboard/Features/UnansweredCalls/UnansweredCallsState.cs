using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.UnansweredCalls;

public record UnansweredCallsState(bool Initialized, IEnumerable<UnansweredCall> UnansweredCalls);