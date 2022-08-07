using CallingDashboard.Models;

namespace CallingDashboard.Features.ActiveCalls;

public record ActiveCallsInitializeAction;

public record ActiveCallsLoadDataAction;

public record ActiveCallsSetDataAction(IEnumerable<CallData> CallData);

public record ActiveCallsSetInitializedAction;

public record ActiveCallsAddAction(CallData CallData);

public record ActiveCallsRemoveAction(string Id);

public record ActiveCallsNotifyAction(NotificationData NotificationData);