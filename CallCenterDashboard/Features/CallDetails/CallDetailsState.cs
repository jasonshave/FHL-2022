using Azure.Communication.CallingServer;

namespace CallCenterDashboard.Features.CallDetails
{
    public record CallDetailsState(string PreviousId, CallConnectionProperties? CallConnectionProperties,  IEnumerable<CallParticipant>? Participants);
}
