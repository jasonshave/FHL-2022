using Fluxor;

namespace CallingDashboard.Features.CallDetails;

public class CallDetailsFeature : Feature<CallDetailsState>
{
    public override string GetName() => "CallDetails";

    protected override CallDetailsState GetInitialState() => new(false, null, null, null);
}
