using System.Collections.Concurrent;
using Azure.Communication.CallingServer;
using CallCenterDashboard.Models;
using Fluxor;

namespace CallCenterDashboard.Features.CallDetails;

public class CallDetailsFeature : Feature<CallDetailsState>
{
    public override string GetName() => "CallDetails";

    protected override CallDetailsState GetInitialState() => new(false, null, null, null);
}
