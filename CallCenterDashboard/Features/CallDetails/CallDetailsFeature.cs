using System.Collections.Concurrent;
using Azure.Communication.CallingServer;
using CallCenterDashboard.Models;
using Fluxor;

namespace CallCenterDashboard.Features.CallDetails;

public class CallDetailsFeature : Feature<CallDetailsState>
{
    public override string GetName() => "CallDetails";

    protected override CallDetailsState GetInitialState() => new(new ConcurrentDictionary<string, bool?>(), new ConcurrentDictionary<string, CallConnectionProperties>(), new ConcurrentDictionary<string, IEnumerable<CallParticipant>>());
}