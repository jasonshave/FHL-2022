using CallingDashboard.Models;
using Fluxor;

namespace CallingDashboard.Features.UnansweredCalls;

public class UnansweredCallsFeature : Feature<UnansweredCallsState>
{
    public override string GetName() => "UnansweredCalls";

    protected override UnansweredCallsState GetInitialState() => new(false, Array.Empty<UnansweredCall>());
}