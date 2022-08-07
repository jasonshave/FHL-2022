using Fluxor;

namespace CallCenterDashboard.Features.AutoAnswer;

public class AutoAnswerFeature : Feature<AutoAnswerState>
{
    public override string GetName() => "AutoAnswer";

    protected override AutoAnswerState GetInitialState() => new(false, false, null);
}