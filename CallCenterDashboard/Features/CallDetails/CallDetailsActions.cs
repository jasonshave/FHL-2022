using Azure.Communication.CallingServer;

namespace CallCenterDashboard.Features.CallDetails;
public record InitializeCallDetailAction(string Id);

public record InitializeCallDetailWithDataAction(
    CallConnectionProperties CallConnectionProperties, 
    IEnumerable<CallParticipant> Participants);


