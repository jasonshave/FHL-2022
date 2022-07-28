using Azure.Communication.CallingServer;
using System.Collections.Concurrent;

namespace CallCenterDashboard.Features.CallDetails
{
    public record CallDetailsState(bool IsInitialized, CallConnectionProperties? CallConnectionProperties,  IEnumerable<CallParticipant>? Participants);
}
