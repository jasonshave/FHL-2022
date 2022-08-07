using CallingDashboard.Models;
using Fluxor;

namespace CallingDashboard.Features.ActiveCalls;

public class ActiveCallsFeature : Feature<ActiveCallsState>
{
    public override string GetName() => "ActiveCalls";

    protected override ActiveCallsState GetInitialState() => new(false, false, Array.Empty<CallData>());
}