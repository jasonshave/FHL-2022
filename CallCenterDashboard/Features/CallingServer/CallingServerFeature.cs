using Fluxor;

namespace CallCenterDashboard.Features.CallingServer;

public class CallingServerFeature : Feature<CallingServerState>
{
    public override string GetName() => "CallingServer";

    protected override CallingServerState GetInitialState() => new (true, false);
}