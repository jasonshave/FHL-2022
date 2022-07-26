using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.ActiveCalls;

public record ActiveCallsLoadDataAction();

public record ActiveCallsSetDataAction(IEnumerable<CallData> CallData);

public record ActiveCallsSetInitializedAction();

public record ActiveCallsAddAction(CallData CallData);

public record ActiveCallsRemoveAction(CallData CallData);

public record ActiveCallsNotifyAction(NotificationData NotificationData);