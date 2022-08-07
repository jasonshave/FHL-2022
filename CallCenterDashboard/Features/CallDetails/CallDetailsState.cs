using Azure.Communication.CallingServer;

namespace CallingDashboard.Features.CallDetails
{
    public record CallDetailsState(bool Initialized, string PreviousId, CallConnectionProperties? CallConnectionProperties,  IEnumerable<CallParticipant>? Participants);
}
