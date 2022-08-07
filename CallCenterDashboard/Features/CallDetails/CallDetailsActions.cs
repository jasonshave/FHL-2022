using Azure.Communication.CallingServer;

namespace CallingDashboard.Features.CallDetails;
public record CallDetailStateInializeAction();
public record CallDetailStateSetInitializationAction();
public record CallDetailInitializeAction(string Id);

public record CallDetailSetDataAction(
    string Id,
    CallConnectionProperties CallConnectionProperties,
    IEnumerable<CallParticipant> Participants);

public record ParticipantUpdateAction(string Id);

public record ParticipantSetDataAction(string Id, IEnumerable<CallParticipant> Participants);

public record CallDetailClearAction(string Id);




