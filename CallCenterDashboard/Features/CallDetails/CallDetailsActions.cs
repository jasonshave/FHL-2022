using Azure.Communication.CallingServer;

namespace CallCenterDashboard.Features.CallDetails;
public record CallDetailInitializeAction(string Id);

public record CallDetailSetDataAction(
    string Id,
    CallConnectionProperties CallConnectionProperties, 
    IEnumerable<CallParticipant> Participants);


