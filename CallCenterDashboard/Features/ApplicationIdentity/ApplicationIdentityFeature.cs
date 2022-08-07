using Fluxor;

namespace CallingDashboard.Features.ApplicationIdentity;

public class ApplicationIdentityFeature : Feature<ApplicationIdentityState>
{
    public override string GetName() => "ApplicationIdentity";

    protected override ApplicationIdentityState GetInitialState() => new(false, false, null, null);
}