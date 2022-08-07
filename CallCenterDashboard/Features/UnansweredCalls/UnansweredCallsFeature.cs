using CallCenterDashboard.Models;
using Fluxor;

namespace CallCenterDashboard.Features.UnansweredCalls;

public class UnansweredCallsFeature : Feature<UnansweredCallsState>
{
    public override string GetName() => "UnansweredCalls";

    protected override UnansweredCallsState GetInitialState() => new(false, Array.Empty<UnansweredCall>());
}