using CallingDashboard.Models;

namespace CallingDashboard.Features.UnansweredCalls;

public record UnansweredCallsInitializeAction;

public record UnansweredCallsAddAction(UnansweredCall UnansweredCall);

public record UnansweredCallsRemoveAction(UnansweredCall UnansweredCall);