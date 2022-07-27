using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.ActiveCalls;

public record ActiveCallsInitializeAction;

public record ActiveCallsLoadDataAction;

public record ActiveCallsSetDataAction(IEnumerable<CallData> CallData);

public record ActiveCallsSetInitializedAction;

public record ActiveCallsAddAction(CallData CallData);

public record ActiveCallsRemoveAction(string CallConnectionId);

public record ActiveCallsNotifyAction(NotificationData NotificationData);