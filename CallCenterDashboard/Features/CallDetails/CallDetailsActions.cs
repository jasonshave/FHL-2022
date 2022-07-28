using Azure.Communication.CallingServer;

namespace CallCenterDashboard.Features.CallDetails;
public record InitializeCallDetailAction(string Id);

public record InitializeCallDetailWithDataAction(string Id,
    CallConnectionProperties CallConnectionProperties, 
    IEnumerable<CallParticipant> Participants);
public record UpdateParticipantsAction(string Id);
public record UpdateParticipantsWithDataAction(string Id, IEnumerable<CallParticipant> Data);
public record ParticipantsUpdatedAction(string Id);
public record RemoveCallDetailAction(string Id);


