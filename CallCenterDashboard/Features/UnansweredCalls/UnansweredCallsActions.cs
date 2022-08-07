using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.UnansweredCalls;

public record UnansweredCallsInitializeAction;

public record UnansweredCallsAddAction(UnansweredCall UnansweredCall);

public record UnansweredCallsRemoveAction(UnansweredCall UnansweredCall);