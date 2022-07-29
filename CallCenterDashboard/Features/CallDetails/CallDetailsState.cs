using Azure.Communication.CallingServer;

namespace CallCenterDashboard.Features.CallDetails
{
    public record CallDetailsState(bool initialized, string PreviousId, CallConnectionProperties? CallConnectionProperties,  IEnumerable<CallParticipant>? Participants);
}
