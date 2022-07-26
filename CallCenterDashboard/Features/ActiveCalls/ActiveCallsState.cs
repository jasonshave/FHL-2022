using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.ActiveCalls;

public record ActiveCallsState(bool Initialized, bool Loading, IEnumerable<CallData> CallData);