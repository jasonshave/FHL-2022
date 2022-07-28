using Azure.Communication.CallingServer;
using System.Collections.Concurrent;

namespace CallCenterDashboard.Features.CallDetails
{
    public record CallDetailsState(string PreviousId, CallConnectionProperties? CallConnectionProperties,  IEnumerable<CallParticipant>? Participants);
}
